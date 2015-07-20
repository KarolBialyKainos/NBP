using System;

namespace WAGTask1.Models
{
    
    public class CurrencyRate
    {
     
        public int ID { get; set; }
        
        public DateTime Date { set; get; }
       
        public int ConversionFactor { set; get; }
        
        public double Rate { set; get; }
        public int CurrencyID { set; get; }


        public virtual Currency Currency { set; get; }

    }
}