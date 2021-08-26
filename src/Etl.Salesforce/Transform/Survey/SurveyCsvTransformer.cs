﻿using System;
using System.Data;

namespace Eol.Cig.Etl.Salesforce.Transform.Survey
{
    public sealed class SurveyCsvTransformer : CsvToDataTableTransformer<Model.Survey>, ISalesforceObjectTransformer
    {
        public DataTable Transform(string data, DateTime insertTime)
        {
            return CsvTransformerHelper.Transform(data, insertTime, GetDataTable<SurveyMap>);
        }
    }
}
