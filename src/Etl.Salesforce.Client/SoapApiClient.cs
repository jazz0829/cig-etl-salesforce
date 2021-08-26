using System;
using System.Collections.Generic;

namespace Eol.Cig.Etl.Salesforce.Client
{
    public class SoapApiClient: IDisposable
    {
        private string UserName { get; set; }
        private string Password { get; set; }
        private string LoginUrl { get; set; }

        SforceService _sfService;
        LoginResult _loginResult;

        public SoapApiClient(string username, string password, string loginUrl)
        {
            UserName = username;
            Password = password;
            LoginUrl = loginUrl;

            Login();
        }

        private void Login()
        {
            _sfService = new SforceService
            {
                Url = LoginUrl
            };
            _loginResult = _sfService.login(UserName, Password);
            _sfService.Url = _loginResult.serverUrl;
        }

        public List<sObject> GetData(string query)
        {
            _sfService.SessionHeaderValue = new SessionHeader() { sessionId = _loginResult.sessionId };

            _sfService.QueryOptionsValue = new QueryOptions { batchSize = 2000, batchSizeSpecified = true };

            _sfService.Url = $"https://{_sfService.Pod}.salesforce.com/services/Soap/c/41.0";

            var list = new List<sObject>();

            var data = _sfService.query(query);
            var done = false;

            if(data.size > 0)
            {
                while (!done)
                {
                    list.AddRange(data.records);

                    if (data.done)
                    {
                        done = true;
                    }
                    else
                    {
                        data = _sfService.queryMore(data.queryLocator);
                    }
                }
            }
            return list;
        }

        public void Dispose()
        {
            if(_sfService != null)
            {
                _sfService.Dispose();
            }
        }
    }
}
