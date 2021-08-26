using System;
using System.Data;
using Eol.Cig.Etl.Shared.Load;

namespace Eol.Cig.Etl.Salesforce.Load
{
    public interface ISalesforceSqlServerUploader: ISqlServerUploader
    {
        /// <summary>
        /// Uploads data to sql server.
        /// </summary>
        /// <param name="data">Date to import</param>
        /// <param name="insertTime">DateTime that will be stored in EtlInsertTime column</param>
        /// <param name="salesforceObjectName">Name of the salesforce object.</param>
        /// <param name="lastBatch">Signal the uploader that this is the last batch. In case there is FullExport, this will make it clean up.</param>
        void Upload(DataTable data, DateTime insertTime, string salesforceObjectName, bool lastBatch = false);

        /// <summary>
        /// Gets LastModifiedDate from Salesforce_DataExportLog
        /// </summary>
        /// <param name="salesforceObjectName">Name of the salesforce object.</param>
        /// <returns>Returns last modified date recoded for provided object or <see cref="DateTime.MinValue"/> if there is no entry for the object.</returns>
        DateTime GetLastModifiedDate(string salesforceObjectName);
    }
}
