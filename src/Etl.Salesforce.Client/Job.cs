using System;
using System.Linq;
using System.Xml.Linq;

namespace Eol.Cig.Etl.Salesforce.Client
{
    public class Job
    {
        public string Id { get; set; }
        public string Operation { get; set; }
        public string Object { get; set; }
        public string CreatedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime SystemModStamp { get; set; }
        public string State { get; set; }
        public string ConcurrencyMode { get; set; }
        public string ContentType { get; set; }
        public int NumberBatchesQueued { get; set; }
        public int NumberBatchesInProgress { get; set; }
        public int NumberBatchesCompleted { get; set; }
        public int NumberBatchesFailed { get; set; }
        public int NumberBatchesTotal { get; set; }
        public int NumberRecordsProcessed { get; set; }
        public int NumberRecordsFailed { get; set; }
        public int NumberRetries { get; set; }
        public int TotalProcessingTime { get; set; }
        public int ApiActiveProcessingTime { get; set; }
        public int ApexProcessingTime { get; set; }

        public static Job Create(string jobXml)
        {
            var job = new Job();
            var doc = XDocument.Parse(jobXml);
            var jobInfoElement = doc.Root;

            if (jobInfoElement != null)
            {
                var jobInfoChildElements = jobInfoElement.Elements().ToList();

                foreach (var e in jobInfoChildElements)
                {
                    switch (e.Name.LocalName)
                    {
                        case "id":
                            job.Id = e.Value;
                            break;
                        case "operation":
                            job.Operation = e.Value;
                            break;
                        case "object":
                            job.Object = e.Value;
                            break;
                        case "createdById":
                            job.CreatedById = e.Value;
                            break;
                        case "createdDate":
                            job.CreatedDate = DateTime.Parse(e.Value);
                            break;
                        case "systemModstamp":
                            job.SystemModStamp = DateTime.Parse(e.Value);
                            break;
                        case "state":
                            job.State = e.Value;
                            break;
                        case "concurrencyMode":
                            job.ConcurrencyMode = e.Value;
                            break;
                        case "contentType":
                            job.ContentType = e.Value;
                            break;
                        case "numberBatchesQueued":
                            job.NumberBatchesQueued = int.Parse(e.Value);
                            break;
                        case "numberBatchesInProgress":
                            job.NumberBatchesInProgress = int.Parse(e.Value);
                            break;
                        case "numberBatchesCompleted":
                            job.NumberBatchesCompleted = int.Parse(e.Value);
                            break;
                        case "numberBatchesFailed":
                            job.NumberBatchesFailed = int.Parse(e.Value);
                            break;
                        case "numberBatchesTotal":
                            job.NumberBatchesTotal = int.Parse(e.Value);
                            break;
                        case "numberRecordsProcessed":
                            job.NumberRecordsProcessed = int.Parse(e.Value);
                            break;
                        case "numberRetries":
                            job.NumberRetries = int.Parse(e.Value);
                            break;
                        case "numberRecordsFailed":
                            job.NumberRecordsFailed = int.Parse(e.Value);
                            break;
                        case "totalProcessingTime":
                            job.TotalProcessingTime = int.Parse(e.Value);
                            break;
                        case "apiActiveProcessingTime":
                            job.ApiActiveProcessingTime = int.Parse(e.Value);
                            break;
                        case "apexProcessingTime":
                            job.ApexProcessingTime = int.Parse(e.Value);
                            break;
                    }
                }
            }

            return job;
        }

        public bool IsDone => NumberBatchesTotal == (NumberBatchesCompleted + NumberBatchesFailed) || State == "Aborted";
    }
}
