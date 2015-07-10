using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WAGTask1.Models;

namespace WAGTask1.DAL
{
    public class CurrencyRatesRepository : BaseBankRepository, ICurrencyRatesRepository
    {
        public CurrencyRatesRepository(BankContext context)
            : base(context)
        {
            
        }

        /// <summary>
        /// Returns rate for given date and currencyID
        /// </summary>
        /// <param name="date"></param>
        /// <param name="currencyID"></param>
        /// <returns></returns>
        public CurrencyRate GetRateForDateAndCurrency(DateTime date, int currencyID)
        {
            CurrencyRate response = null;
            if (context.CurrencyRates.Any(r => r.Date == date && r.CurrencyID == currencyID))
            {
                response = context.CurrencyRates.FirstOrDefault(r => r.Date == date && r.CurrencyID == currencyID);

            }
            return response;
        }
    }
}