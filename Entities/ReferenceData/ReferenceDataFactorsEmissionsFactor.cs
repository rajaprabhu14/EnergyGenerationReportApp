using System.Xml.Serialization;

namespace EnergyReportGenerationApp.Entities.ReferenceData
{
    public class ReferenceDataFactorsEmissionsFactor
    {
        [XmlElement("High")]
        public decimal? High { get; set; }

        [XmlElement("Medium")]
        public decimal? Medium { get; set; }

        [XmlElement("Low")]
        public decimal? Low { get; set; }
    }
}
