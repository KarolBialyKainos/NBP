using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WAGTask1.Models;

namespace WAGTask1.DAL
{
    interface ICurrencyRatesRepository : IDisposable
    {
        CurrencyRate GetRateForDateAndCurrency(DateTime date, int currencyID);
    }
}
