using System.Collections.Generic;

namespace WAGTask1.Models
{

    public class Currency
    {

        public int ID { get; set; }
        public string Name { set; get; }
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