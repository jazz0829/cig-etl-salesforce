using System;
using System.Data;
using System.Data.SqlClient;
using Eol.Cig.Etl.Shared.Configuration;
using Eol.Cig.Etl.Shared.Load;
using Eol.Cig.Etl.Shared.Utils;
using Eol.Cig.Etl.Salesforce.Extract;
using log4net;

namespace Eol.Cig.Etl.Salesforce.Load
{
    public class SalesforceSqlServerUploader : SqlServerUploader, ISalesforceSqlServerUploader
    {
        private bool _isFullExport;

        public SalesforceSqlServerUploader(ILog logger,
            ISqlJobConfiguration configuration)
            : base(logger, configuration)
        { }

        public void Upload(DataTable data, DateTime insertTime, string salesforceObjectName, bool lastBatch = false)
        {
            using (var conn = Retry.Do(() => SqlServerUtils.OpenConnection(Configuration.DestinationConnectionString), TimeSpan.FromSeconds(60), 5))
            {
                using (var tran = conn.BeginTransaction())
                {
                    BulkInsert(data, conn, tran, Configuration.DestinationTable);
                    UpdateLastModifiedDate(salesforceObjectName, insertTime, conn, tran);
                    if (lastBatch && _isFullExport)
                    {
                        FinishFullExport(salesforceObjectName, insertTime, conn, tran);
                    }
                    tran.Commit();
                }
            }
        }

        public DateTime GetLastModifiedDate(string objectName)
        {
            CheckIfFullExportIsScheduled(objectName);
            return FindLastModifiedDate(objectName);
        }

        private DateTime FindLastModifiedDate(string objectName)
        {
            var dateModified = DateTime.MinValue;

            var tableName = "config.Salesforce_DataExportLog";
            if (_isFullExport)
            {
                tableName = "config.Salesforce_FullExport";
            }

            var sqlQuery = $@"SELECT MAX(LastModifiedDate) as LastModifiedDate FROM {tableName} WHERE ObjectName = '{objectName}'";

            using (var reader = SqlServerUtils.ExecuteCommandReturnReader(sqlQuery, Configuration.DestinationConnectionString))
            {
                if (reader != null && reader.HasRows)
                {
                    reader.Read();
                    dateModified = reader.IsDBNull(0) ? DateTime.MinValue : reader.GetDateTime(0);
                    Logger.Info($"LastModifiedDate for object {objectName} was {dateModified.ToSalesforceDateTimeFormat()}");
                }
                else
                {
                    Logger.Info($"There were no records in the export log for object: {dateModified}");
                }
            }

            return dateModified;
        }

        private void UpdateLastModifiedDate(string objectName, DateTime insertTime, SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            var readQuery = objectName.ToUpper() == "KEYEVENT__C" || objectName.ToUpper() == "CASEHISTORY" ? $@"SELECT MAX(CreatedDate) AS LastModifiedDate FROM {Configuration.DestinationTable}" : $@"SELECT MAX(LastModifiedDate) AS LastModifiedDate FROM {Configuration.DestinationTable}";

            var logTable = "config.Salesforce_DataExportLog";

            //If we are doing a full export, we log progress in a different table.
            //When full export is done that progress will be deleted and a single entry writen to the regular log table
            if (_isFullExport)
            {
                //Since we alredy have most of the records from previous exports
                //we need to check only the lates records we inserted
                //>= is because we don't want to compare milliseconds
                //Since we just inserted it there is very little change there is another process with the insert time in the same second
                readQuery += $" WHERE EtlInsertTime >= '{insertTime.ToString("yyyy-MM-dd HH:mm:ss")}' AND EtlInsertTime < '{insertTime.AddSeconds(1).ToString("yyyy-MM-dd HH:mm:ss")}'";
                logTable = "config.Salesforce_FullExport";
            }

            var lastDate = SqlServerUtils.ExecuteCommandReturnSingle<DateTime>(readQuery, sqlConnection, sqlTransaction);

            var updateQuery = $@"INSERT INTO {logTable} (ObjectName, InsertTime, LastModifiedDate) VALUES(@ObjectName,@InsertTime,@LastModifiedDate)";

            var objectNameParam = new SqlParameter("ObjectName", SqlDbType.NVarChar);
            var insertTimeParam = new SqlParameter("InsertTime", SqlDbType.DateTime);
            var lastModifiedDateParam = new SqlParameter("LastModifiedDate", SqlDbType.DateTime);

            objectNameParam.Value = objectName;
            insertTimeParam.Value = insertTime;
            lastModifiedDateParam.Value = lastDate;

            SqlServerUtils.ExecuteCommandReturnNone(updateQuery, sqlConnection, sqlTransaction, objectNameParam, insertTimeParam, lastModifiedDateParam);
        }

        private void CheckIfFullExportIsScheduled(string salesforceObjectName)
        {
            /* Full export is scheduled by inserting object name into config.Salesforce_FullExport table.
             * That table is used to store the progress of full export.
             * Once it's done, that table is cleaned (for the object in question) and a standard record is inserted
             * (config.Salesforce_DataExportLog)
             */

            var sqlQuery = $@"SELECT TOP 1 ObjectName FROM config.Salesforce_FullExport WHERE ObjectName = '{salesforceObjectName}'";

            using (var reader = SqlServerUtils.ExecuteCommandReturnReader(sqlQuery, Configuration.DestinationConnectionString))
            {
                if (reader != null && reader.HasRows)
                {
                    _isFullExport = true;
                    Logger.Info("Full export was scheduled.");
                }
                else
                {
                    Logger.Info("Full export is not needed. Moving on.");
                }
            }
        }

        public void FinishFullExport(string salesforceObjectName, DateTime insertTime, SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            //Delete rows in config.Salesforce_FullExport for the object and update DataExportLog
            var deleteCommand = $"DELETE FROM config.Salesforce_FullExport WHERE ObjectName ='{salesforceObjectName}'";
            SqlServerUtils.ExecuteCommandReturnNone(deleteCommand, sqlConnection, sqlTransaction);
            _isFullExport = false;
            UpdateLastModifiedDate(salesforceObjectName, insertTime, sqlConnection, sqlTransaction);
        }
    }
}