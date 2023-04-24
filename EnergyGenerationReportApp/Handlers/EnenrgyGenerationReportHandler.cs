using System;
using System.IO;
using EnergyReportGenerationApp.Entities;
using EnergyReportGenerationApp.Entities.GenerationReportInput;
using EnergyReportGenerationApp.Entities.GenerationReportOutput;
using EnergyReportGenerationApp.Entities.ReferenceData;
using EnergyReportGenerationApp.Interfaces;
using Microsoft.Extensions.Configuration;
using Topshelf.Logging;

namespace EnergyReportGenerationApp.Handlers
{
    /// <summary>
    /// EnenrgyGenerationReportHandler class for Handling business logic.
    /// </summary>
    public class EnenrgyGenerationReportHandler : IEnenrgyGenerationReportHandler
    {
        private readonly IFileHandler _fileHandler;
        private readonly IXmlSerializationHandler _xmlSerializationHandler;
        private readonly IEnergyDataProcessingHandler _energyDataProcessingHandler;
        private readonly IConfiguration _configuration;
        private readonly LogWriter logWriter;

        private ReportSettings _reportSettings;

        public EnenrgyGenerationReportHandler(
            IFileHandler fileHandler,
            IXmlSerializationHandler xmlSerializationHandler,
            IEnergyDataProcessingHandler energyDataProcessingHandler,
            IConfiguration configuration)
        {
            _fileHandler = fileHandler;
            _xmlSerializationHandler = xmlSerializationHandler;
            _energyDataProcessingHandler = energyDataProcessingHandler;
            _configuration = configuration;
            _reportSettings = _configuration.GetSection(nameof(ReportSettings)).Get<ReportSettings>();
            logWriter = HostLogger.Get<ReportGenerationProcessor>();
        }

        public void ProcessEnergyGenerationReport(string filePath)
        {
            try
            {
                if (_fileHandler.IsXmlvalid(filePath) && _fileHandler.IsFileReadyForProcessing(filePath))
                {
                    logWriter.Info($"Report generation started the file {filePath}");

                    var generationReportIntputDetail = _xmlSerializationHandler.DeserializeToObject<GenerationReportIntputDetail>(filePath);
                    var referenceDataDetail = _xmlSerializationHandler.DeserializeToObject<ReferenceDataDetail>(_reportSettings.ReferenceDataPath);

                    var generationReportOutputDetail = _energyDataProcessingHandler.ProcessEnergyGenerationData(generationReportIntputDetail, referenceDataDetail);

                    var outputXmlFullFilePath = $"{_reportSettings.OutputFolderPath}{Path.GetFileNameWithoutExtension(filePath)}{_reportSettings.OutputFileSuffix}";

                    _xmlSerializationHandler.SerializeToXml<GenerationReportOutputDetail>(generationReportOutputDetail, outputXmlFullFilePath);

                    _fileHandler.MoveFileToFolder(filePath, _reportSettings.ProcessedFolderPath, Path.GetFileName(filePath));

                    logWriter.Info($"input file Moved to Processed folder{filePath}");
                }
            }
            catch (Exception exception)
            {
                logWriter.Error($"Exception ocuurend while processting the file {filePath}");
                logWriter.Error($"Exception:{exception.Message}");
                _fileHandler.MoveFileToFolder(filePath, _reportSettings.ErrorFolderPath, Path.GetFileName(filePath));
            }
        }
    }
}
