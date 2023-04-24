namespace EnergyReportGenerationApp.Interfaces
{
    public interface IFileHandler
    {
        bool IsFileReadyForProcessing(string filePath);

        bool IsXmlvalid(string filepath);

        void MoveFileToFolder(string sourceFilePath, string destinationPath, string destinationFileName);

        void CheckAndCreateDirecory(string path);
    }
}
