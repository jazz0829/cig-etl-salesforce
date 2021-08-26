using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Threading;
using System.Xml.Linq;

namespace Eol.Cig.Etl.Salesforce.Client
{
    /// <summary>
    /// Wrapper class to Salesforce's Bulk API.
    /// </summary>
    /// <seealso cref="!:https://www.salesforce.com/us/developer/docs/api_asynch/" />

    public class BulkApiClient : IDisposable
    {
        private string UserName { get; set; }
        private string Password { get; set; }
        private string LoginUrl { get; set; }

        SforceService _sfService;
        LoginResult _loginResult;

        public BulkApiClient(string username, string password, string loginUrl)
        {
            UserName = username;
            Password = password;
            LoginUrl = loginUrl;

            Login();
        }

        public Job CreateJob(CreateJobRequest createJobRequest)
        {
            var jobRequestXml =
            @"<?xml version=""1.0"" encoding=""UTF-8""?>
             <jobInfo xmlns=""http://www.force.com/2009/06/asyncapi/dataload"">
               <operation>{0}</operation>
               <object>{1}</object>
               {3}
               <contentType>{2}</contentType>
             </jobInfo>";

            var externalField = string.Empty;

            if (!string.IsNullOrWhiteSpace(createJobRequest.ExternalIdFieldName))
            {
                externalField = "<externalIdFieldName>" + createJobRequest.ExternalIdFieldName + "</externalIdFieldName>";
            }

            jobRequestXml = string.Format(jobRequestXml,
                                          createJobRequest.OperationString,
                                          createJobRequest.Object,
                                          createJobRequest.ContentTypeString,
                                          externalField);

            var createJobUrl = "https://" + _sfService.Pod + ".salesforce.com/services/async/41.0/job";

            var resultXml = InvokeRestApi(createJobUrl, jobRequestXml);

            return Job.Create(resultXml);
        }

        public Job CloseJob(string jobId)
        {
            var closeJobUrl = BuildSpecificJobUrl(jobId);
            var closeRequestXml =
            @"<?xml version=""1.0"" encoding=""UTF-8""?>" + Environment.NewLine +
            @"<jobInfo xmlns=""http://www.force.com/2009/06/asyncapi/dataload"">" + Environment.NewLine +
             "<state>Closed</state>" + Environment.NewLine +
             "</jobInfo>";

            var resultXml = InvokeRestApi(closeJobUrl, closeRequestXml);

            return Job.Create(resultXml);
        }

        public Job GetJob(string jobId)
        {
            var getJobUrl = BuildSpecificJobUrl(jobId);

            var resultXml = InvokeRestApi(getJobUrl);

            return Job.Create(resultXml);
        }

        public Job GetCompletedJob(string jobId)
        {
            var job = GetJob(jobId);

            while (!job.IsDone)
            {
                Thread.Sleep(2000);
                job = GetJob(jobId);
            }

            return job;
        }

        public Batch CreateAttachmentBatch(CreateAttachmentBatchRequest request)
        {
            var requestTxtFileCsvContents = "Name,ParentId,Body" + Environment.NewLine;
            requestTxtFileCsvContents += request.FilePath + "," + request.ParentId + ",#" + request.FilePath;

            using (var memoryStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    var requestTxtFileCsvContentsBytes = Encoding.UTF8.GetBytes(requestTxtFileCsvContents);
                    var requestTxtFileInArchive = archive.CreateEntry("request.txt");
                    using (var entryStream = requestTxtFileInArchive.Open())
                    using (var fileToCompressStream = new MemoryStream(requestTxtFileCsvContentsBytes))
                    {
                        fileToCompressStream.CopyTo(entryStream);
                    }

                    var attachmentFileContentsBytes = File.ReadAllBytes(request.FilePath);
                    var attachmentFileInArchive = archive.CreateEntry(request.FilePath);
                    using (var attachmentEntryStream = attachmentFileInArchive.Open())
                    using (var attachmentFileToCompressStream = new MemoryStream(attachmentFileContentsBytes))
                    {
                        attachmentFileToCompressStream.CopyTo(attachmentEntryStream);
                    }

                    var zipFileBytes = memoryStream.ToArray();

                    var requestUrl = "https://" + _sfService.Pod + ".salesforce.com/services/async/41.0/job/" + request.JobId + "/batch";

                    var responseBytes = InvokeRestApi(requestUrl, zipFileBytes, "POST", "zip/csv");

                    var resultXml = Encoding.UTF8.GetString(responseBytes);

                    return Batch.CreateBatch(resultXml);
                }
            }
        }

        public Batch CreateBatch(CreateBatchRequest createBatchRequest)
        {
            var requestUrl = "https://" + _sfService.Pod + ".salesforce.com/services/async/41.0/job/" + createBatchRequest.JobId + "/batch";

            var requestXml = createBatchRequest.BatchContents;

            var contentType = string.Empty;

            if (createBatchRequest.ContentType.HasValue)
            {
                contentType = createBatchRequest.BatchContentHeader;
            }

            var resultXml = InvokeRestApi(requestUrl, requestXml, "Post", contentType);

            return Batch.CreateBatch(resultXml);
        }

        public Batch GetBatch(string jobId, string batchId)
        {
            var requestUrl = "https://" + _sfService.Pod + ".salesforce.com/services/async/41.0/job/" + jobId + "/batch/" + batchId;

            var resultXml = InvokeRestApi(requestUrl);

            return Batch.CreateBatch(resultXml);
        }

        public List<Batch> GetBatches(string jobId)
        {
            var requestUrl = "https://" + _sfService.Pod + ".salesforce.com/services/async/41.0/job/" + jobId + "/batch/";

            var resultXml = InvokeRestApi(requestUrl);

            return Batch.CreateBatches(resultXml);
        }

        public string GetBatchRequest(string jobId, string batchId)
        {
            var requestUrl = "https://" + _sfService.Pod + ".salesforce.com/services/async/41.0/job/" + jobId + "/batch/" + batchId + "/request";

            var resultXml = InvokeRestApi(requestUrl);

            return resultXml;
        }

        public string GetBatchResults(string jobId, string batchId)
        {
            var requestUrl = "https://" + _sfService.Pod + ".salesforce.com/services/async/41.0/job/" + jobId + "/batch/" + batchId + "/result";

            var resultXml = InvokeRestApi(requestUrl);

            return resultXml;
        }

        public List<string> GetResultIds(string queryBatchResultListXml)
        {
            //<result-list xmlns="http://www.force.com/2009/06/asyncapi/dataload"><result>752x000000000F1</result></result-list>

            var doc = XDocument.Parse(queryBatchResultListXml);
            var resultIds = new List<string>();

            var resultListElement = doc.Root;

            if (resultListElement != null)
            {
                foreach (var resultElement in resultListElement.Elements())
                {
                    var resultId = resultElement.Value;
                    resultIds.Add(resultId);
                }
            }

            return resultIds;
        }

        public string GetBatchResult(string jobId, string batchId, string resultId)
        {
            var requestUrl = "https://" + _sfService.Pod + ".salesforce.com/services/async/41.0/job/" + jobId + "/batch/" + batchId + "/result/" + resultId;

            var resultXml = InvokeRestApi(requestUrl);

            return resultXml;
        }

        private string BuildSpecificJobUrl(string jobId)
        {
            return "https://" + _sfService.Pod + ".salesforce.com/services/async/41.0/job/" + jobId;
        }

        private void Login()
        {
            _sfService = new SforceService();
            var queryOptions = new QueryOptions
            {
                batchSize = 5000,
                batchSizeSpecified = true
            };
            _sfService.QueryOptionsValue = queryOptions;

            _sfService.Url = LoginUrl;
            _loginResult = _sfService.login(UserName, Password);
            _sfService.Url = _loginResult.serverUrl;
        }

        private string InvokeRestApi(string endpointUrl)
        {
            var wc = BuildWebClient();

            return wc.DownloadString(endpointUrl);
        }

        private string InvokeRestApi(string endpointUrl, string postData)
        {
            return InvokeRestApi(endpointUrl, postData, "Post", string.Empty);
        }

        private string InvokeRestApi(string endpointUrl, string postData, string httpVerb, string contentType)
        {
            var postDataBytes = Encoding.UTF8.GetBytes(postData);

            var response = InvokeRestApi(endpointUrl, postDataBytes, httpVerb, contentType);

            return Encoding.UTF8.GetString(response);
        }

        private byte[] InvokeRestApi(string endpointUrl, byte[] postData, string httpVerb, string contentType)
        {
            var wc = BuildWebClient();

            if (!string.IsNullOrWhiteSpace(contentType))
            {
                wc.Headers.Add("Content-Type: " + contentType);
            }

            try
            {
                return wc.UploadData(endpointUrl, httpVerb, postData);
            }
            catch (WebException webEx)
            {
                if (webEx.Response != null)
                {
                    using (var errorResponse = (HttpWebResponse)webEx.Response)
                    {
                        using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                        {
                            throw new Exception(reader.ReadToEnd()); 
                        }
                    }
                }

                throw;
            }
        }

        private WebClient BuildWebClient()
        {
            var wc = new WebClient { Encoding = Encoding.UTF8 };
            wc.Headers.Add("X-SFDC-Session: " + _loginResult.sessionId);

            return wc;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                _sfService.Dispose();
            }
        }
    }
}
