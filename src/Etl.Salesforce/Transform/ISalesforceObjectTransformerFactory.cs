namespace Eol.Cig.Etl.Salesforce.Transform
{
    public interface ISalesforceObjectTransformerFactory
    {
        ISalesforceObjectTransformer CreateTransformer(string salesforceObject);
    }
}