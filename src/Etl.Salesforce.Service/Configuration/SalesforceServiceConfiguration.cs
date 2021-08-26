using System.Collections.Specialized;
using Eol.Cig.Etl.Shared.Configuration;
using Eol.Cig.Etl.Shared.Extensions;

namespace Eol.Cig.Etl.Salesforce.Service.Configuration
{
    public class SalesforceServiceConfiguration: ServiceConfiguration, ISalesforceServiceConfiguration
    {
        public SalesforceServiceConfiguration(NameValueCollection appSettings) : base(appSettings)
        {
            Schedule = appSettings.GetStringOrThrow("SCHEDULE");
            TwiceADaySchedule = appSettings.GetStringOrThrow("TWICEADAYSCHEDULE");
            GeneralUploadSchedule = appSettings.GetStringOrThrow("GENERALUPLOADSCHEDULE");
            KeyEventsUploadSchedule = appSettings.GetStringOrThrow("KEYEVENTSUPLOADSCHEDULE");
            CaseReasonsUploadSchedule = appSettings.GetStringOrThrow("CASEREASONSUPLOADSCHEDULE");
        }
        
        public string Schedule { get; }
        public string TwiceADaySchedule { get; }
        public string GeneralUploadSchedule { get; }
        public string KeyEventsUploadSchedule { get; }
        public string CaseReasonsUploadSchedule { get; set; }
    }
}
