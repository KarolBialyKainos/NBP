using System;
using System.Collections.Generic;
using System.Web.Mvc;
using WAGTask1.Models;

namespace WAGTask1.Controllers
{
    public class HomeController : Controller
    {
        NBPController NBPWebController;

        public HomeController()
        {
            NBPWebController = new NBPController();
        }

        public ActionResult Index()
        {
            List<Currency> currencies = NBPWebController.Currencies();
            if (currencies.Count == 0)
            {//if the DB is empty then retrieve latest rates and fill db with list of currencies
                NBPWebController.RetrieveRatePositionsForSpecificDateAndUpdateDb(DateTime.Now);
                currencies = NBPWebController.Currencies();
            }
            ViewBag.CurrencyID = new SelectList(currencies, "ID", "NameWithCode");
            NBPLibrary.NBPXMLReader r = new NBPLibrary.NBPXMLReader();
   
            return View();
        }

        public ActionResult Trend(string id, int CurrencyID)
        {
           
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("Invalid Date");
            }

            if (CurrencyID <= 0)
            {
                throw new ArgumentException("Invalid Currency ID");
            }

            try {
                DateTime chosenDate;
                chosenDate = DateTime.Parse(id);
                ViewBag.Date = chosenDate;
                ViewBag.Trend = NBPWebController.RateWithTrendForDateAndCurrency(CurrencyID, chosenDate);
            }catch(System.FormatException )
            {
                ViewBag.Message = "Unknown date";
            }
            catch (System.Net.WebException )
            {
                ViewBag.Message = "No data for chosen date";
            }
            ViewBag.Currency = NBPWebController.Currency(CurrencyID);
          
            return View();
        }
    }
}