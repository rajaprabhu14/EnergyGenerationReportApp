namespace EnergyReportGenerationApp.Interfaces
{
    public interface IXmlSerializationHandler
    {
        T DeserializeToObject<T>(string filepath);

        void SerializeToXml<T>(T anyobject, string xmlFilePath);
    }
}
