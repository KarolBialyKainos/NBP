using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using WAGTask1.Models;

namespace WAGTask1.DAL
{
    public class BankContext : DbContext, IBankContext
    {
        public BankContext()
            : base("BankContext")
        {

        }

        public DbSet<Currency> Currencies { get; set; }
        public DbSet<CurrencyRate> CurrencyRates { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}