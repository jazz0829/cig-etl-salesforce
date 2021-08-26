using CsvHelper.Configuration;

namespace Eol.Cig.Etl.Salesforce.Common
{
    public sealed class AccountUploadResultMap: CsvClassMap<AccountUploadResult>
    {
        public AccountUploadResultMap()
        {
            AutoMap();
            Map(c => c.RowNumber).Ignore();
        }
    }
}
