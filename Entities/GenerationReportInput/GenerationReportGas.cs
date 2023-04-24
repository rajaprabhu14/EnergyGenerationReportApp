using System.Collections.Generic;
using System.Xml.Serialization;

namespace EnergyReportGenerationApp.Entities.GenerationReportInput
{
    public class GenerationReportGas
    {
        [XmlElement("GasGenerator")]
        public List<BaseGenerator> GasGenerator { get; set; }
    }
}
