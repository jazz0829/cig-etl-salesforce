namespace Eol.Cig.Etl.Salesforce.Configuration.Common
{
    public interface ISalesforceSettings
    {
        string Username { get; }
        string Password { get; }
        string SecurityToken { get; }
        string LoginUrl { get; }
    }
}
