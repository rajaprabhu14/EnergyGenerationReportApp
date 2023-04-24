using System;
using System.IO;
using System.Threading;
using EnergyReportGenerationApp.Entities;
using EnergyReportGenerationApp.Interfaces;
using Microsoft.Extensions.Configuration;
using Topshelf.Logging;

namespace EnergyReportGenerationApp
{
    /// <summary>
    /// ReportGenerationProcessor class for registering filewatcher events.
    /// </summary>
    public class ReportGenerationProcessor : IReportGenerationProcessor
    {
        private readonly IConfiguration _configuration;
        private readonly IEnenrgyGenerationReportHandler _enenrgyGenerationReportHandler;
        private readonly LogWriter logWriter;
        private readonly IFileHandler _fileHandler;

        private ReportSettings _reportSettings;

        public ReportGenerationProcessor(IConfiguration configuration, IEnenrgyGenerationReportHandler enenrgyGenerationReportHandler, IFileHandler fileHandler)
        {
            _configuration = configuration;
            _reportSettings = _configuration.GetSection(nameof(ReportSettings)).Get<ReportSettings>();
            _enenrgyGenerationReportHandler = enenrgyGenerationReportHandler;
            logWriter = HostLogger.Get<ReportGenerationProcessor>();
            _fileHandler = fileHandler;
        }

        /// <summary>
        /// Register FileSystemWatcher events to moniter Input direcory.
        /// </summary>
        public void RegisterEvents()
        {
            // create all the directories for placing the files.
            CreateDirectories();

            var watcher = new FileSystemWatcher(_reportSettings.InputFolderPath, _reportSettings.InputFileExtension)
            {
                InternalBufferSize = 65536,
                IncludeSubdirectories = false,
                NotifyFilter = NotifyFilters.Attributes |
                               NotifyFilters.CreationTime |
                               NotifyFilters.DirectoryName |
                               NotifyFilters.FileName |
                               NotifyFilters.LastAccess |
                               NotifyFilters.LastWrite |
                               NotifyFilters.Size
            };

            var fileThread = new Thread(() =>
             {
                 watcher.Created += OnCreated;
                 watcher.Error += OnWatcherError;
                 watcher.EnableRaisingEvents = true;
             });

            fileThread.Start();

            logWriter.Info($"Registered Files sytem watcher for input folder.");

            // Process files already in Input folder
            RaiseEventsForNotProcessedFiles();
        }

        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            logWriter.Info($"File Created event triggered for file {e.Name}");
            _enenrgyGenerationReportHandler.ProcessEnergyGenerationReport(e.FullPath);
        }

        private void OnWatcherError(object sender, ErrorEventArgs e)
        {
            logWriter.Error($"File watcher possible might encountered buffer overflow exception on Input folder.{e.GetException()}");
            Console.WriteLine("Error");
        }

        private void RaiseEventsForNotProcessedFiles()
        {
            var directoryinfo = new DirectoryInfo(_reportSettings.InputFolderPath);
            foreach (var fileInfo in directoryinfo.GetFiles(_reportSettings.InputFileExtension, SearchOption.TopDirectoryOnly))
            {
                var fileSysytemEventArgument = new FileSystemEventArgs(WatcherChangeTypes.Created, _reportSettings.InputFolderPath, fileInfo.Name);
                OnCreated(this, fileSysytemEventArgument);
            }
        }

        private void CreateDirectories()
        {
            _fileHandler.CheckAndCreateDirecory(_reportSettings.InputFolderPath);
            _fileHandler.CheckAndCreateDirecory(_reportSettings.ErrorFolderPath);
            _fileHandler.CheckAndCreateDirecory(_reportSettings.OutputFolderPath);
            _fileHandler.CheckAndCreateDirecory(_reportSettings.ProcessedFolderPath);
        }
    }
}
