using System;
using System.Xml.Serialization;

namespace EnergyReportGenerationApp.Entities.GenerationReportInput
{
    [XmlType(AnonymousType = true)]
    [Serializable]
    [XmlRoot("GenerationReport")]
    public class GenerationReportIntputDetail
    {
        [XmlElement("Wind")]
        public GenerationReportWind Wind { get; set; }
        [XmlElement("Gas")]
        public GenerationReportGas Gas { get; set; }

        [XmlElement("Coal")]
        public GenerationReportCoal Coal { get; set; }
    }
}
