namespace EnergyReportGenerationApp.Entities
{
    public class ReportSettings
    {
        public string InputFolderPath { get; set; }
        public string OutputFolderPath { get; set; }
        public string InputFileExtension { get; set; }
        public string OutputFileSuffix { get; set; }
        public string ProcessedFolderPath { get; set; }
        public string ErrorFolderPath { get; set; }
        public string ReferenceDataPath { get; set; }
    }
}
