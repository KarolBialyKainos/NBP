using NBPLibrary;
using NBPLibrary.Models;

namespace WAGTask1.Tests.Mock
{
    public class FakeNBPReader : INBPReader
    {
        public RatePositions GetRatePositionsForSpecificDate(System.DateTime date)
        {
            System.Collections.Generic.List<RatePosition> positions = new System.Collections.Generic.List<RatePosition>();
            positions.Add(new RatePosition(){AverageRate = 99.512,CalculationFactor = 1,CurrencyCode = "CUR1",CurrencyName="Test currency 1"});
            positions.Add(new RatePosition(){AverageRate = 99.512,CalculationFactor = 1,CurrencyCode = "CUR2",CurrencyName="Test currency 2"});

            RatePositions ratePositions = new RatePositions()
            {
                Positions = positions
            };

            return ratePositions;
        }
    }
}
