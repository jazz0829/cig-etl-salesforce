using System;

namespace Eol.Cig.Etl.Salesforce.Model
{
    public class WebActivity : SalesforceObject
    {
        public static readonly string ObjectName = "WEB_ACTIVITY__C";
        public static readonly string Query = @"SELECT AccountID__c,Account_Status__c,Campaigne__c,Category__c,changeCode__c,City__c,Company_Name__c,ContactID__c,
                                                Country__c,CreatedById,CreatedDate,CurrencyIsoCode,Dateofcall__c,Department__c,Disclaimer__c,dnb_addressRegion__c,
                                                dnb_cityName__c,dnb_countryName__c,dnb_duns__c,dnb_isdCode__c,dnb_isMailUndeliverable__c,dnb_isoAlpha2Code__c,
                                                dnb_isUnreachable__c,dnb_numberOfEmployees__c,dnb_operatingStatus__c,dnb_postalCode__c,dnb_primaryName__c,
                                                dnb_streetAddress__c,dnb_telephoneNumber__c,dnb_telephone__c,dnb_tradeStyleNames__c,dnb_usSicV4Description__c,
                                                dnb_usSicV4__c,dnb_websiteAddress__c,dnb_yearlyRevenue__c,egnewsletter__c,egproductinformation__c,EmailAddress__c,
                                                EOLAdministration__c,EOLOpportunityID__c,Extra_Field1__c,Extra_Field2__c,Extra_Field3__c,Extra_Field4__c,Extra_Field5__c,
                                                First_Name__c,FormID__c,Form_Label__c,funnel_stage__c,GAID__c,gclid__c,Gender__c,Id,IsDeleted,IsProbablyAccountant__c,
                                                JobTitle__c,Language__c,LastModifiedById,LastModifiedDate,LastReferencedDate,LastViewedDate,Last_Name__c,MiddleName__c,
                                                Name,NewWebActivity__c,Notes__c,Opportunity_Lead_Type__c,OptIn__c,OwnerId,Paid__c,PersonalEmail__c,PersonalMessage__c,
                                                Phone__c,PostalCode__c,Project__c,Question__c,Salutation__c,Source__c,SYNAdministration__c,SynReRoute__c,SystemModstamp,
                                                Timeslot__c,touchpoint__c,username__c,utm_campaign__c,utm_content__c,utm_end__c,utm_medium__c,utm_referrer__c,
                                                utm_source__c,utm_start__c,utm_term__c FROM Web_Activity__c";

        public string AccountID__c { get; set; }
        public string Account_Status__c { get; set; }
        public string Campaigne__c { get; set; }
        public string Category__c { get; set; }
        public string changeCode__c { get; set; }
        public string City__c { get; set; }
        public string Company_Name__c { get; set; }
        public string ContactID__c { get; set; }
        public string Country__c { get; set; }
        public string CreatedById { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CurrencyIsoCode { get; set; }
        public string Dateofcall__c { get; set; }
        public string Department__c { get; set; }
        public string Disclaimer__c { get; set; }
        public string dnb_addressRegion__c { get; set; }
        public string dnb_cityName__c { get; set; }
        public string dnb_countryName__c { get; set; }
        public string dnb_duns__c { get; set; }
        public string dnb_isdCode__c { get; set; }
        public string dnb_isMailUndeliverable__c { get; set; }
        public string dnb_isoAlpha2Code__c { get; set; }
        public string dnb_isUnreachable__c { get; set; }
        public string dnb_numberOfEmployees__c { get; set; }
        public string dnb_operatingStatus__c { get; set; }
        public string dnb_postalCode__c { get; set; }
        public string dnb_primaryName__c { get; set; }
        public string dnb_streetAddress__c { get; set; }
        public string dnb_telephoneNumber__c { get; set; }
        public string dnb_telephone__c { get; set; }
        public string dnb_tradeStyleNames__c { get; set; }
        public string dnb_usSicV4Description__c { get; set; }
        public string dnb_usSicV4__c { get; set; }
        public string dnb_websiteAddress__c { get; set; }
        public string dnb_yearlyRevenue__c { get; set; }
        public string egnewsletter__c { get; set; }
        public string egproductinformation__c { get; set; }
        public string EmailAddress__c { get; set; }
        public string EOLAdministration__c { get; set; }
        public string EOLOpportunityID__c { get; set; }
        public string Extra_Field1__c { get; set; }
        public string Extra_Field2__c { get; set; }
        public string Extra_Field3__c { get; set; }
        public string Extra_Field4__c { get; set; }
        public string Extra_Field5__c { get; set; }
        public string First_Name__c { get; set; }
        public string FormID__c { get; set; }
        public string Form_Label__c { get; set; }
        public string funnel_stage__c { get; set; }
        public string GAID__c { get; set; }
        public string gclid__c { get; set; }
        public string Gender__c { get; set; }
        public string Id { get; set; }
        public string IsDeleted { get; set; }
        public string IsProbablyAccountant__c { get; set; }
        public string JobTitle__c { get; set; }
        public string Language__c { get; set; }
        public string LastModifiedById { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public DateTime? LastReferencedDate { get; set; }
        public DateTime? LastViewedDate { get; set; }
        public string Last_Name__c { get; set; }
        public string MiddleName__c { get; set; }
        public string Name { get; set; }
        public string NewWebActivity__c { get; set; }
        public string Notes__c { get; set; }
        public string Opportunity_Lead_Type__c { get; set; }
        public string OptIn__c { get; set; }
        public string OwnerId { get; set; }
        public string Paid__c { get; set; }
        public string PersonalEmail__c { get; set; }
        public string PersonalMessage__c { get; set; }
        public string Phone__c { get; set; }
        public string PostalCode__c { get; set; }
        public string Project__c { get; set; }
        public string Question__c { get; set; }
        public string Salutation__c { get; set; }
        public string Source__c { get; set; }
        public string SYNAdministration__c { get; set; }
        public string SynReRoute__c { get; set; }
        public string SystemModstamp { get; set; }
        public string Timeslot__c { get; set; }
        public string touchpoint__c { get; set; }
        public string username__c { get; set; }
        public string utm_campaign__c { get; set; }
        public string utm_content__c { get; set; }
        public string utm_end__c { get; set; }
        public string utm_medium__c { get; set; }
        public string utm_referrer__c { get; set; }
        public string utm_source__c { get; set; }
        public string utm_start__c { get; set; }
        public string utm_term__c { get; set; }
    }
}
