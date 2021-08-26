namespace Eol.Cig.Etl.Salesforce.Common
{
    public class AccountUploadResult
    {
        public int RowNumber { get; set; }
        public string Id { get; set; }
        public bool Success { get; set; }
        public bool Created { get; set; }
        public string Error { get; set; }

        public override string ToString() => $"Salesfore response ==> Id: {Id}\t Success: {Success}\t Created: {Created} \t Error: {Error}";
    }
}
