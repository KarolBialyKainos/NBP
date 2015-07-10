using NBPLibrary.Models;
using System;

namespace NBPLibrary
{
    public interface INBPReader
    {
        RatePositions GetRatePositionsForSpecificDate(DateTime date);
    }
}
