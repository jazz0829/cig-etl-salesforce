using System;

namespace Eol.Cig.Etl.Salesforce.Tests
{
    public class SalesforceObjectException : Exception
    {
        public SalesforceObjectException(string message) : base(message)
        {
        }
    }
}
