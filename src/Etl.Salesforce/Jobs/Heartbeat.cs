using log4net;
using System;
using System.Threading.Tasks;

namespace Eol.Cig.Etl.Salesforce.Jobs
{
    public class Heartbeat : IEtlJob
    {
        private readonly ILog _logger;

        public static readonly string Name = nameof(Heartbeat);

        public Heartbeat(ILog logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task Execute()
        {
            _logger.Info("Salesforce service is running.");
            return Task.CompletedTask;
        }

        public string GetName()
        {
            return Name;
        }
    }
}
