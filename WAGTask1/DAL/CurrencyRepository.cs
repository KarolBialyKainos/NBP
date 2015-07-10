using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WAGTask1.DAL
{
    public class CurrencyRepository : BaseBankRepository, ICurrencyRepository
    {
        

        public CurrencyRepository(BankContext context) : base(context)
        {
            
        }

        public IEnumerable<Models.Currency> GetCurrencies()
        {
            return context.Currencies.ToList();
        }

        public Models.Currency GetCurrencyByID(int currencyID)
        {
            return context.Currencies.Find(currencyID);
        }

        
    }
}