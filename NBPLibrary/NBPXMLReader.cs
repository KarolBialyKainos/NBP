using NBPLibrary.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml.Serialization;

namespace NBPLibrary
{
    public class NBPXMLReader : INBPReader
    {
        /// <summary>
        /// Address of web service
        /// </summary>
        public static String BaseRateURL = "http://www.nbp.pl/kursy/xml/";

        /// <summary>
        /// Number of tries to download web service
        /// </summary>
        private static int NumberOfTriesAllowed = 5;

        #region Helper methods

        /// <summary>
        /// Construct url for given table name
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        protected string ConstructRateUrl(string tableName)
        {
            return string.Format("{0}{1}.xml", BaseRateURL, tableName);
        }

        /// <summary>
        /// Retrieve all available rates
        /// </summary>
        /// <returns></returns>
        protected string[] GetTableNames(int year)
        {
            WebClient client = new WebClient();

            string FileName = string.Format("dir{0}.txt", year == DateTime.Now.Year ? "" : year.ToString());

            string response = client.DownloadString(BaseRateURL + FileName);
            char[] delimiters = new char[] { '\n' };
            string[] tableNames = response.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
            return tableNames;

        }

        /// <summary>
        /// Return table name with rates for given date. If there is no table name for specific day, the method will look for most previous day
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        protected string GetTableNameForSpecificDay(DateTime date)
        {
            string response = null;
            string[] tableNames = GetTableNames(date.Year);
            List<string> list = new List<string>(tableNames);

            string tableString;

            int numberOfTries = 0;



            do{
                date = date.AddDays(-numberOfTries);
                tableString= date.ToString("yyMMdd");
                response = list.FirstOrDefault(s => s.StartsWith("a") && s.Substring(5, 6).Equals(tableString));
                numberOfTries++;
            } while (string.IsNullOrEmpty(response) && numberOfTries < NumberOfTriesAllowed);
            
            if( response != null )
                response = response.Trim();

            return response;
        }

        /// <summary>
        /// Retrieve rates XML from given URL address
        /// </summary>
        /// <param name="url">Full URL of XML with rates</param>
        /// <returns></returns>
        protected RatePositions GetRatePositionFromURL(string url)
        {
            var encoding = Encoding.GetEncoding("windows-1250");
            XmlSerializer deserializer = new XmlSerializer(typeof(RatePositions));
            StreamReader reader = new StreamReader(WebRequest.Create(url).GetResponse().GetResponseStream(),encoding);
            object obj = deserializer.Deserialize(reader);
            RatePositions XmlData = (RatePositions)obj;
            reader.Close();

            return XmlData;
        }

        #endregion

        /// <summary>
        /// Return RatePositions for latest table
        /// </summary>
        /// <returns></returns>
        public RatePositions GetLatestRatePositions()
        {
            RatePositions result = GetRatePositionFromURL(ConstructRateUrl("LastA"));
            return result;
        }

        /// <summary>
        /// Retrieve table with rates for specific date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public RatePositions GetRatePositionsForSpecificDate(DateTime date){
            string tableName = null;

            tableName = GetTableNameForSpecificDay(date);

            RatePositions result = GetRatePositionFromURL(ConstructRateUrl(tableName));
            return result;
        }
    }
}
