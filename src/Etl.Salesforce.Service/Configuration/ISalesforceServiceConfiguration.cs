using Eol.Cig.Etl.Shared.Configuration;

namespace Eol.Cig.Etl.Salesforce.Service.Configuration
{
    public interface ISalesforceServiceConfiguration: IServiceConfiguration
    {
        string Schedule { get; }
        string TwiceADaySchedule { get; }
        string GeneralUploadSchedule { get; }
        string KeyEventsUploadSchedule { get; }
        string CaseReasonsUploadSchedule { get; }
    }
}
