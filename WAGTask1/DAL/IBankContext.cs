using System.Data.Entity;
using WAGTask1.Models;

namespace WAGTask1.DAL
{
    public interface IBankContext
    {
        DbSet<Currency> Currencies { get; set; }
        DbSet<CurrencyRate> CurrencyRates { get; set; }
        int SaveChanges();
    }
}
