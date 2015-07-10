using NBPLibrary;
using NBPLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WAGTask1.DAL;
using WAGTask1.Models;

namespace WAGTask1.Controllers
{
    public class NBPController : ApiController
    {
        protected IBankContext context;
        protected INBPReader NBPReader;

        public NBPController(IBankContext context, INBPReader NBPReader)
        {
            this.context = context;
            this.NBPReader = NBPReader;
        }

        public NBPController(IBankContext context) : this(context,new NBPXMLReader())
        {
            
        }

        public NBPController()
            : base()
        {
            this.context = new BankContext();
            //disable creating of proxies
            (context as BankContext).Configuration.ProxyCreationEnabled = false;
            NBPReader = new NBPXMLReader();
        }

        /// <summary>
        /// Retrieve list of all currencies from Local DB
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<Models.Currency> Currencies()
        {
            return context.Currencies.ToList();
        }


        /// <summary>
        /// Return single currency by ID
        /// </summary>
        /// <param name="id">ID of currency</param>
        /// <returns>Action Result with currency</returns>
        [HttpGet]
        public Currency Currency(int id)
        {
            Currency cur = context.Currencies.FirstOrDefault(c => c.ID == id);
            return cur;
        }

        /// <summary>
        /// Retrieve rate for given specific currency and date from Local DB
        /// </summary>
        /// <param name="id">Currency ID</param>
        /// <param name="date">Chosen date</param>
        /// <returns></returns>
        [HttpGet]
        public CurrencyRate RateForDateAndCurrency(int id, DateTime date)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Wrong id parameter");
            }

            CurrencyRate currency = null;

            if (context.CurrencyRates.Any(r => r.Date == date && r.CurrencyID == id))
            {
                currency = context.CurrencyRates.FirstOrDefault(r => r.Date == date && r.CurrencyID == id);
            }
            return currency;
        }

        /// <summary>
        /// Retrieve trend for rate for given date and the day before
        /// </summary>
        /// <param name="id">Currency ID</param>
        /// <param name="date">Chosen date</param>
        /// <returns></returns>
        [HttpGet]
        public CurrencyTrend RateWithTrendForDateAndCurrency(int id, DateTime date)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Wrong id parameter");
            }

            //get rates for chosen date
            CurrencyRate rate = RateForDateAndCurrency(id, date);
            if (rate == null)
            {//if rates are not in local DB then call web service
                RetrieveRatePositionsForSpecificDateAndUpdateDb(date);
                rate = RateForDateAndCurrency(id, date);
            }

            //get rates for previous day
            CurrencyRate previousRate = RateForDateAndCurrency(id,date.AddDays(-1));
            if (previousRate == null)
            {//if rates are not in local DB then call web service
                RetrieveRatePositionsForSpecificDateAndUpdateDb(date.AddDays(-1));
                previousRate = RateForDateAndCurrency(id, date.AddDays(-1));
            }


            CurrencyTrend trend = new CurrencyTrend()
            {
                Rate = rate,
                PreviousRate = previousRate
            };

            return trend;
        }

        /// <summary>
        /// Retrieve from web service currency rates for sepcific date and add them to DB
        /// </summary>
        /// <param name="date">Date of currency rates</param>
        public void RetrieveRatePositionsForSpecificDateAndUpdateDb(DateTime date)
        {
            //retrieve rates from web service
            RatePositions rates = NBPReader.GetRatePositionsForSpecificDate(date);

            //add rates to DB
            CurrencyRate rate;
            foreach (RatePosition position in rates.Positions)
            {
                rate = MapRatePositionToCurrencyRate(position,date);
                context.CurrencyRates.Add(rate);
            }
            context.SaveChanges();
        }


        /// <summary>
        /// Map results from NBP web service to Currency Rate Entity
        /// </summary>
        /// <param name="position">Currency Rate Position from NBP</param>
        /// <param name="date">Date of currency rates</param>
        /// <returns></returns>
        private CurrencyRate MapRatePositionToCurrencyRate(RatePosition position, DateTime date)
        {
            //find currency, if doesn't exist then add it to DB
            Currency currency = CurrencyByCode(position.CurrencyCode);

            if( currency == null ){
                //add new currency to DB
                currency = new Currency(){
                    Name = position.CurrencyName,
                    Code = position.CurrencyCode
                };
                context.Currencies.Add(currency);
                context.SaveChanges();
            }

            CurrencyRate rate = new CurrencyRate()
            {
                ConversionFactor = position.CalculationFactor,
                Rate = position.AverageRate,
                Currency = currency,
                Date = date
            };

            return rate;
        }




        

        /// <summary>
        /// Return single currency by ID
        /// </summary>
        /// <param name="id">ID of currency</param>
        /// <returns>Action Result with currency</returns>
        [HttpGet]
        public Currency CurrencyByCode(string code)
        {
            Currency cur = context.Currencies.FirstOrDefault(c => c.Code.Equals(code.ToUpper()));
            return cur;
        }
    }
}
