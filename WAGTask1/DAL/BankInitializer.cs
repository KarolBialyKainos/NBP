using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WAGTask1.Models;

namespace WAGTask1.DAL
{
    public class BankInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<BankContext>
    {
        protected override void Seed(BankContext context)
        {
            base.Seed(context);
            var currencies = new List<Currency>()
            {
                new Currency(){ Code = "USD",Name = "dolar amerykański" },
                new Currency(){ Code = "EUR",Name = "euro" },
                new Currency(){ Code = "CHF",Name = "frank szwajcarski" }
            };

            currencies.ForEach(c => context.Currencies.Add(c));
            context.SaveChanges();

            var usd = new List<CurrencyRate>()
            {
                new CurrencyRate(){ ConversionFactor=1,CurrencyID = 1, Rate = 3.671, Date = DateTime.Parse("2015-06-29")},
                new CurrencyRate(){ ConversionFactor=1,CurrencyID = 1, Rate = 3.571, Date = DateTime.Parse("2015-06-28")},
                new CurrencyRate(){ ConversionFactor=1,CurrencyID = 1, Rate = 4.671, Date = DateTime.Parse("2015-06-27")},
                new CurrencyRate(){ ConversionFactor=1,CurrencyID = 1, Rate = 2.671, Date = DateTime.Parse("2015-06-26")},
            };

            usd.ForEach(r => context.CurrencyRates.Add(r));
            context.SaveChanges();

            var euro = new List<CurrencyRate>()
            {
                new CurrencyRate(){ ConversionFactor=1,CurrencyID = 2, Rate = 4.1893, Date = DateTime.Parse("2015-06-29")},
                new CurrencyRate(){ ConversionFactor=1,CurrencyID = 2, Rate = 4.571, Date = DateTime.Parse("2015-06-28")},
                new CurrencyRate(){ ConversionFactor=1,CurrencyID = 2, Rate = 4.231, Date = DateTime.Parse("2015-06-27")},
                new CurrencyRate(){ ConversionFactor=1,CurrencyID = 2, Rate = 3.971, Date = DateTime.Parse("2015-06-26")},
            };

            euro.ForEach(r => context.CurrencyRates.Add(r));
            context.SaveChanges();

            var chf = new List<CurrencyRate>()
            {
                new CurrencyRate(){ ConversionFactor=1,CurrencyID = 3, Rate = 4.0208, Date = DateTime.Parse("2015-06-29")},
                new CurrencyRate(){ ConversionFactor=1,CurrencyID = 3, Rate = 4.071, Date = DateTime.Parse("2015-06-28")},
                new CurrencyRate(){ ConversionFactor=1,CurrencyID = 3, Rate = 4.0431, Date = DateTime.Parse("2015-06-27")},
                new CurrencyRate(){ ConversionFactor=1,CurrencyID = 3, Rate = 4.371, Date = DateTime.Parse("2015-06-26")},
            };

            chf.ForEach(r => context.CurrencyRates.Add(r));
            context.SaveChanges();

        }
    }
}