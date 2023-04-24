using EnergyReportGenerationApp.Interfaces;
using Topshelf;
using Topshelf.Logging;

namespace EnergyReportGenerationApp.Service
{
    /// <summary>
    /// EnergyReportGenerationService class
    /// </summary>
    public class EnergyReportGenerationService : ServiceControl
    {
        private readonly LogWriter logWriter;
        private readonly IReportGenerationProcessor _reportGenerationProcessor;

        public EnergyReportGenerationService(IReportGenerationProcessor reportGenerationProcessor)
        {
            _reportGenerationProcessor = reportGenerationProcessor;
            logWriter = HostLogger.Get<EnergyReportGenerationService>();
        }

        public bool Start(HostControl hostControl)
        {
            logWriter.Info("Service started");
            _reportGenerationProcessor.RegisterEvents();
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            logWriter.Info("Service stopped");
            return true;
        }
    }
}
