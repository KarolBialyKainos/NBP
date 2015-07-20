using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using WAGTask1.Controllers;
using WAGTask1.Models;
using WAGTask1.Tests.DAL;
using WAGTask1.Tests.Mock;

namespace WAGTask1.Tests.Controllers
{
    [TestClass]
    public class NBPControllerTest
    {
        private FakeBankContext GetFakeBankContextWithCurrencies()
        {
            FakeBankContext mockContext = new FakeBankContext();
            mockContext.Currencies.Add(new Currency() { ID = 1, Code = "USD" });
            mockContext.Currencies.Add(new Currency() { ID = 2, Code = "EUR" });
            mockContext.Currencies.Add(new Currency() { ID = 3, Code = "GBP" });
            return mockContext;
        }

        private FakeBankContext GetFakeBankContextWithCurrenciesAndRates()
        {
            FakeBankContext mockContext = GetFakeBankContextWithCurrencies();

            DateTime date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
     
            mockContext.CurrencyRates.Add(new CurrencyRate() { ID = 1, ConversionFactor = 1, Date = date, Rate = 4.523, Currency = mockContext.Currencies.FirstOrDefault(c => c.ID == 1), CurrencyID = 1 });
            mockContext.CurrencyRates.Add(new CurrencyRate() { ID = 2, ConversionFactor = 1, Date = date.AddDays(-1), Rate = 4.234, Currency = mockContext.Currencies.FirstOrDefault(c => c.ID == 1), CurrencyID = 1 });
            mockContext.CurrencyRates.Add(new CurrencyRate() { ID = 3, ConversionFactor = 1, Date = date.AddDays(-2), Rate = 4.534, Currency = mockContext.Currencies.FirstOrDefault(c => c.ID == 1), CurrencyID = 1 });
            mockContext.CurrencyRates.Add(new CurrencyRate() { ID = 4, ConversionFactor = 1, Date = date.AddDays(-3), Rate = 4.534, Currency = mockContext.Currencies.FirstOrDefault(c => c.ID == 1), CurrencyID = 1 });
            return mockContext;
        }


        [TestMethod]
        public void Currencies_ReturnsAllCurrencies()
        {
            //Arrange
            FakeBankContext mockContext = GetFakeBankContextWithCurrencies();
            NBPController controller = new NBPController(mockContext);

            //Act
            IEnumerable<Currency> result = controller.Currencies();

            //Assert
            Assert.AreEqual(result.Count<Currency>(), mockContext.Currencies.Count<Currency>());
        }

        [TestMethod]
        public void Currency_ReturnsSingleCurrency()
        {
            //Arrange 
            NBPController controller = new NBPController(GetFakeBankContextWithCurrencies());

            //Act
            Currency result = controller.Currency(1);

            //Assert
            Assert.AreEqual(result.ID, 1);
        }

        [TestMethod]
        public void CurrencyByCode_ReturnsSingleCurrencyByCurrencyCode()
        {
            //Arrange 
            NBPController controller = new NBPController(GetFakeBankContextWithCurrencies());
            string lookForCurrency = "USD";
            //Act
            Currency result = controller.CurrencyByCode(lookForCurrency);

            //Assert
            Assert.AreEqual(lookForCurrency, result.Code);
        }

        [TestMethod]
        public void RateForDateAndCurrency_ReturnsCurrencyRateForGivenCurrencyAndDate(){

            //Arrange
            DateTime date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            FakeBankContext context = GetFakeBankContextWithCurrenciesAndRates();
            NBPController controller = new NBPController(context);

            //Act
            CurrencyRate result1 = controller.RateForDateAndCurrency(1, date);

            //Assert
            Assert.AreEqual(result1.Rate, context.CurrencyRates.FirstOrDefault( c => c.ID == 1).Rate);
            Assert.AreEqual(result1.ID, context.CurrencyRates.FirstOrDefault(c => c.ID == 1).ID);
        }

        [TestMethod]
        public void RateWithTrendForDateAndCurrency_ReturnsCurrencyTrendForSpecificCurrencyAndDate()
        {

            //Arrange
            DateTime date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            FakeBankContext context = GetFakeBankContextWithCurrenciesAndRates();
            FakeNBPReader NBPReader = new FakeNBPReader();
            NBPController controller = new NBPController(context,NBPReader);
            

            //Act
            CurrencyTrend result1 = controller.RateWithTrendForDateAndCurrency(1, date);
            CurrencyTrend result2 = controller.RateWithTrendForDateAndCurrency(1, date.AddDays(-1));
            CurrencyTrend result3 = controller.RateWithTrendForDateAndCurrency(1, date.AddDays(-2));
            CurrencyTrend result4 = controller.RateWithTrendForDateAndCurrency(1, date.AddDays(-10));

            //Assert
            Assert.AreEqual(result1.Trend, CurrencyTrendResult.UP);
            Assert.AreEqual(result2.Trend, CurrencyTrendResult.DOWN);
            Assert.AreEqual(result3.Trend, CurrencyTrendResult.CONST);

            Assert.IsTrue(context.Currencies.Any(c => c.Code == "CUR1"));
            Assert.IsTrue(context.CurrencyRates.Any(c => c.Rate == 99.512));
            
        }
    }
}
