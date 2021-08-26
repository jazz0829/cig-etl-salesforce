using System;

namespace Eol.Cig.Etl.Salesforce.Model
{
    public class Survey : SalesforceObject
    {
        public static readonly string ObjectName = "SURVEY__C";
        public static readonly string Query = @"SELECT Answer_1__c,Answer_2__c,Answer_3__c,Answer_4__c,Answer_5__c,Answer_6__c,Answer_7__c,Answer_8__c,Average_A1_A4__c,Average_A4_7__c,Case__c," +
                                              @"Converted_Answer_1__c,Converted_Answer_2__c,Converted_Answer_3__c,Converted_Answer_4__c,Converted_Answer_5__c,Converted_Answer_6__c," + 
                                              @"Converted_Answer_7__c,CreatedById,CreatedDate,CurrencyIsoCode,Detractor__c,Id,IsDeleted,LastActivityDate,LastModifiedById,LastModifiedDate,Name," + 
                                              @"New_Answer_1__c,New_Answer_2__c,New_Answer_3__c,New_Answer_4__c,New_Answer_5__c,New_Converted_Answer_1__c,New_Converted_Answer_4__c," + 
                                              @"New_Question_1__c,New_Question_2__c,New_Question_3__c,New_Question_4__c,New_Question_5__c,Promoter__c,Question_1__c,Question_2__c,Question_3__c," + 
                                              @"Question_4__c,Question_5__c,Question_6__c,Question_7__c,Question_8__c FROM Survey__c";

        public string Id { get; set; }
        public int? Answer_1__c { get; set; }
        public int? Answer_2__c { get; set; }
        public int? Answer_3__c { get; set; }
        public int? Answer_4__c { get; set; }
        public int? Answer_5__c { get; set; }
        public int? Answer_6__c { get; set; }
        public int? Answer_7__c { get; set; }
        public string Answer_8__c { get; set; }
        public float Average_A1_A4__c { get; set; }
        public float Average_A4_7__c { get; set; }
        public string Case__c { get; set; }
        public float Converted_Answer_1__c { get; set; }
        public float Converted_Answer_2__c { get; set; }
        public float Converted_Answer_3__c { get; set; }
        public float Converted_Answer_4__c { get; set; }
        public float Converted_Answer_5__c { get; set; }
        public float Converted_Answer_6__c { get; set; }
        public float Converted_Answer_7__c { get; set; }
        public string CreatedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CurrencyIsoCode { get; set; }
        public bool Detractor__c { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? LastActivityDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string LastModifiedById { get; set; }
        public string Name { get; set; }
        public int? New_Answer_1__c { get; set; }
        public string New_Answer_2__c { get; set; }
        public string New_Answer_3__c { get; set; }
        public int? New_Answer_4__c { get; set; }
        public string New_Answer_5__c { get; set; }
        public float New_Converted_Answer_1__c { get; set; }
        public float New_Converted_Answer_4__c { get; set; }
        public string New_Question_1__c { get; set; }
        public string New_Question_2__c { get; set; }
        public string New_Question_3__c { get; set; }
        public string New_Question_4__c { get; set; }
        public string New_Question_5__c { get; set; }
        public bool Promoter__c { get; set; }
        public string Question_1__c { get; set; }
        public string Question_2__c { get; set; }
        public string Question_3__c { get; set; }
        public string Question_4__c { get; set; }
        public string Question_5__c { get; set; }
        public string Question_6__c { get; set; }
        public string Question_7__c { get; set; }
        public string Question_8__c { get; set; }
    }
}
