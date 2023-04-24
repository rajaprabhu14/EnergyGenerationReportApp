using System;
using System.IO;
using System.Threading;
using System.Xml.Linq;
using EnergyReportGenerationApp.Interfaces;
using Topshelf.Logging;

namespace EnergyReportGenerationApp.Handlers
{
    public class FileHandler : IFileHandler
    {
        private readonly LogWriter logWriter;

        public FileHandler()
        {
            logWriter = HostLogger.Get<ReportGenerationProcessor>();
        }

        /// <summary>
        /// Method to check if the file is ready for processing.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public bool IsFileReadyForProcessing(string filePath)
        {
            bool isFileReady = false;
            int fileAccessMaxCheck = 5;
            int loopIndex = 0;

            while (!isFileReady && loopIndex < fileAccessMaxCheck)
            {
                loopIndex++;

                try
                {
                    using var fs = File.Open(filePath, FileMode.Open);
                    isFileReady = fs.CanRead;
                }
                catch (IOException)
                {
                    isFileReady = false;
                    Thread.Sleep(1000);
                }
            }
            return isFileReady;
        }

        public bool IsXmlvalid(string filePath)
        {
            bool isValid;
            try
            {
                XDocument doc = new XDocument();
                doc = XDocument.Load(filePath);
                isValid = true;
            }
            catch (Exception exception)
            {
                logWriter.Error($"Invalid Xml {filePath}");
                logWriter.Error(exception.Message);
                throw;
            }

            return isValid;
        }

        public void MoveFileToFolder(string sourceFilePath, string destinationPath, string destinationFileName)
        {
            File.Move(sourceFilePath, $"{destinationPath}\\{destinationFileName}");
        }

        public void CheckAndCreateDirecory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}
