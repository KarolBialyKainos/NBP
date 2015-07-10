using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WAGTask1.DAL;
using WAGTask1.Models;

namespace WAGTask1.Tests.DAL
{
    public class FakeBankContext : IBankContext
    {
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<CurrencyRate> CurrencyRates { get; set; }

        public FakeBankContext()
        {
            Currencies = new FakeDbSet<Currency>();
            CurrencyRates = new FakeDbSet<CurrencyRate>();
        }

        public int SaveChanges()
        {
            return 1;
        }
    }
}
