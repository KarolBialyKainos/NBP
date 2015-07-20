
namespace WAGTask1.Models
{
    public class CurrencyTrend
    {
        public CurrencyRate Rate { set; get; }
        public CurrencyRate PreviousRate { set; get; }

        public CurrencyTrendResult Trend
        {
            get{
                if (Rate.Rate > PreviousRate.Rate)
                {
                    return CurrencyTrendResult.UP;
                }
                else if (Rate.Rate == PreviousRate.Rate)
                {
                    return CurrencyTrendResult.CONST;
                }
                else
                {
                    return CurrencyTrendResult.DOWN;
                }
            }
        }
    }
}