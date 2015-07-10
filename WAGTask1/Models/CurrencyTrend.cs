
namespace WAGTask1.Models
{
    public class CurrencyTrend
    {
        public CurrencyRate Rate { set; get; }
        public CurrencyRate PreviousRate { set; get; }

        public int Trend
        {
            get{
                if (Rate.Rate > PreviousRate.Rate)
                {
                    return 1;
                }
                else if (Rate.Rate == PreviousRate.Rate)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
        }
    }
}