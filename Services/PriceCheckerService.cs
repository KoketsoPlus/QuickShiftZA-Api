using System;

using QuickShiftZA.Api.Models;

namespace QuickShiftZA.Api.Services;

public class PriceCheckerService
{
    public string CheckPrice(decimal proposedPrice, PriceBenchmark benchmark)
    {
        if (proposedPrice < benchmark.MinPrice)
            return "too_low";

        if (proposedPrice > benchmark.MaxPrice)
            return "too_high";

        return "fair";
    }
}
