using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml.Serialization;

namespace WAGTask1.Models
{
    //[DataContract]
    public class Currency
    {
       // [DataMember]
        public int ID { get; set; }
       // [DataMember]
        public string Name { set; get; }
       // [DataMember]
        public string Code { set; get; }

        public string NameWithCode
        {
            get
            {
                return string.Format("{0} {1}", Code, Name);
            }
        }

        public virtual ICollection<CurrencyRate> Rates { set; get; }

        
        public override string ToString()
        {
            return NameWithCode;
        }
    }
}