using System;
using System.IO;
using System.Xml.Serialization;
using EnergyReportGenerationApp.Interfaces;
using Topshelf.Logging;

namespace EnergyReportGenerationApp.Handlers
{
    /// <summary>
    /// XmlSerializationHandler class to convert xml to object and vice versa.
    /// </summary>
    public class XmlSerializationHandler : IXmlSerializationHandler
    {
        private readonly LogWriter logWriter;

        public XmlSerializationHandler()
        {
            logWriter = HostLogger.Get<ReportGenerationProcessor>();
        }

        /// <summary>
        /// Method to Deserialize xml in to object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public T DeserializeToObject<T>(string filepath)
        {
            try
            {
                var ser = new XmlSerializer(typeof(T));

                using StreamReader sr = new StreamReader(filepath);
                return (T)ser.Deserialize(sr);
            }
            catch (Exception exception)
            {
                logWriter.Error($"Error while deserialising object from XML.{exception.Message}");
                throw;
            }
        }

        /// <summary>
        /// Method to serialize object and create xml.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="anyobject"></param>
        /// <param name="xmlFilePath"></param>
        public void SerializeToXml<T>(T anyobject, string xmlFilePath)
        {
            try
            {
                var xmlSerializer = new XmlSerializer(anyobject.GetType());

                using StreamWriter writer = new StreamWriter(xmlFilePath);
                xmlSerializer.Serialize(writer, anyobject);
            }
            catch (Exception exception)
            {
                logWriter.Error($"Error while generating XML.{exception.Message}");
                throw;
            }
        }
    }
}
