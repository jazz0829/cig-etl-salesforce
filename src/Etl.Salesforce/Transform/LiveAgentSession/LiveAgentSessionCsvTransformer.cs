using System;
using System.Data;
using Eol.Cig.Etl.Salesforce.Transform.LiveAgentSession;

namespace Eol.Cig.Etl.Salesforce.Transform.LiveAgentsession
{
    public sealed class LiveAgentSessionCsvTransformer: CsvToDataTableTransformer<Model.LiveAgentSession>, ISalesforceObjectTransformer
    {
        public DataTable Transform(string data, DateTime insertTime)
        {
            return CsvTransformerHelper.Transform(data, insertTime, GetDataTable<LiveAgentSessionMap>);
        }
    }
}
