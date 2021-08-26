using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using Eol.Cig.Etl.Salesforce.Model;
using System.Linq;
using Xunit;

namespace Eol.Cig.Etl.Salesforce.Tests
{
    public class VerifySalesforceObjects
    {
        private readonly List<Type> _salesforceObjects;

        public VerifySalesforceObjects()
        {
            _salesforceObjects = new List<Type>();
            _salesforceObjects = Assembly.GetAssembly(typeof (SalesforceObject)).GetTypes()
                .Where(t => t.IsClass &&
                            !t.IsAbstract &&
                            t.IsSubclassOf(typeof (SalesforceObject))).ToList();
        }

        [Fact]
        public void Given_Salesforce_Object_Then_Property_ObjectName_NeedsToBeSet_And_Uppercase()
        {
            var exceptions = new List<Exception>();

            foreach (var salesforceObject in _salesforceObjects)
            {
                var objectNameProperty = salesforceObject.GetField("ObjectName");

                if (objectNameProperty == null)
                {
                    exceptions.Add(
                        new SalesforceObjectException(
                            $"ObjectName not defined for salesforce object {salesforceObject.Name}. You should define public const string ObjectName that will hold the name of this Salesforce object."));
                    continue;
                }

                var objectName = (string) objectNameProperty.GetValue(null);

                if (string.IsNullOrWhiteSpace(objectName))
                {
                    exceptions.Add(new SalesforceObjectException($"ObjectName not set for salesforce object {salesforceObject.Name}"));
                }

                if (objectName == null || objectName != objectName.ToUpper(CultureInfo.InvariantCulture))
                {
                    exceptions.Add(new SalesforceObjectException($"ObjectName is not uppercase. It is recommended to keep it uppercase"));
                }
            }

            Assert.Empty(exceptions);
        }

        [Fact]
        public void Given_Salesforce_Object_Then_Property_Query_NeedsToBeSet()
        {
            var exceptions = new List<Exception>();

            foreach (var salesforceObject in _salesforceObjects)
            {
                var queryProperty = salesforceObject.GetField("Query");

                if (queryProperty == null)
                {
                    exceptions.Add(
                        new SalesforceObjectException(
                            $"Query is not defined for salesforce object {salesforceObject.Name}.  You should define public const string Query that will hold the query used to populate this Salesforce object."));
                    continue;
                }

                var query = (string) queryProperty.GetValue(null);

                if (string.IsNullOrWhiteSpace(query))
                {
                    exceptions.Add(new SalesforceObjectException($"Query not set for salesforce object {salesforceObject.Name}"));
                }
            }

            if (exceptions.Any())
            {
                throw new AggregateException(exceptions);
            }
        }

        [Fact]
        public void Given_Salesforce_Object_Then_Properties_In_The_Object_Should_Exactly_Correspond_To_The_Query()
        {
            var exceptions = new List<Exception>();

            foreach (var salesforceObject in _salesforceObjects)
            {
                var properties = salesforceObject.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(p => p.CanWrite && p.CanRead)
                    .Select(p => p.Name ).ToArray();
                var query = (string)salesforceObject.GetField("Query").GetValue(null);
                var queryFields = GetQueryFields(query);

                var diff = queryFields.Except(properties).ToList();
                if (diff.Any())
                {
                    var propertiesNotMatching = diff.Aggregate((a, s) => a + ", " + s);
                    exceptions.Add(
                        new SalesforceObjectException(
                            $"Query for Salesforce object {salesforceObject.Name} does not match object properties. " +
                            $"This will result in imprort/export errors. Properties not mathing: {propertiesNotMatching}"));
                }
            }

            if (exceptions.Any())
            {
                throw new AggregateException(exceptions);
            }
        }

        private IEnumerable<string> GetQueryFields(string query)
        {
            var indexOfSelect = query.IndexOf("select", StringComparison.OrdinalIgnoreCase);

            if (indexOfSelect < 0)
            {
                throw new SalesforceObjectException("Expected a SELECT query in Salesforce object.");
            }

            var firstIndexAfterSelect = indexOfSelect + "select".Length;
            var indexOfFrom = query.IndexOf("from ", StringComparison.OrdinalIgnoreCase);

            if (indexOfFrom < 0)
            {
                throw new SalesforceObjectException("Query is missing a FROM statement.");
            }

            var fieldsString = query.Substring(firstIndexAfterSelect, indexOfFrom - firstIndexAfterSelect);

            return fieldsString.Split(',').Select(s => s.Trim());
        }
    }
}
