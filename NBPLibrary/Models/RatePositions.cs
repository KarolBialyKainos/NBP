using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace NBPLibrary.Models
{
    [Serializable]
    [XmlRoot("tabela_kursow"),XmlType("tabela_kursow")]
    public class RatePositions
    {
        [XmlElement("numer_tabeli")]
        public string TableNumber { set; get; }
        [XmlElement("data_publikacji")]
        public string PublicationDate { set; get; }
        [XmlElement("pozycja")]
        public List<RatePosition> Positions { set; get; }
    }
}
