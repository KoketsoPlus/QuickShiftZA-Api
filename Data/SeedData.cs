using QuickShiftZA.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace QuickShiftZA.Api.Data;

public static class SeedData
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (await context.PriceBenchmarks.AnyAsync()) return;

        var benchmarks = new List<PriceBenchmark>
        {
            // Hairdressing
            new PriceBenchmark { Category = "Hairdressing", Area = "Johannesburg", MinPrice = 150, MaxPrice = 800 },
            new PriceBenchmark { Category = "Hairdressing", Area = "Cape Town", MinPrice = 200, MaxPrice = 1000 },
            new PriceBenchmark { Category = "Hairdressing", Area = "Durban", MinPrice = 150, MaxPrice = 750 },
            new PriceBenchmark { Category = "Hairdressing", Area = "Pretoria", MinPrice = 150, MaxPrice = 700 },
            new PriceBenchmark { Category = "Hairdressing", Area = "Soweto", MinPrice = 80, MaxPrice = 400 },

            // Laundry
            new PriceBenchmark { Category = "Hairdressing", Area = "Johannesburg", MinPrice = 150, MaxPrice = 800 },
            new PriceBenchmark { Category = "Hairdressing", Area = "Cape Town", MinPrice = 200, MaxPrice = 1000 },
            new PriceBenchmark { Category = "Hairdressing", Area = "Durban", MinPrice = 150, MaxPrice = 750 },
            new PriceBenchmark { Category = "Hairdressing", Area = "Pretoria", MinPrice = 150, MaxPrice = 700 },
            new PriceBenchmark { Category = "Hairdressing", Area = "Soweto", MinPrice = 80, MaxPrice = 400 },

            // Catering
            new PriceBenchmark { Category = "Catering", Area = "Johannesburg", MinPrice = 500, MaxPrice = 5000 },
            new PriceBenchmark { Category = "Catering", Area = "Cape Town", MinPrice = 600, MaxPrice = 6000 },
            new PriceBenchmark { Category = "Catering", Area = "Durban", MinPrice = 400, MaxPrice = 4500 },
            new PriceBenchmark { Category = "Catering", Area = "Pretoria", MinPrice = 400, MaxPrice = 4000 },
            new PriceBenchmark { Category = "Catering", Area = "Soweto", MinPrice = 300, MaxPrice = 3000 },

            // Cleaning
            new PriceBenchmark { Category = "Cleaning", Area = "Johannesburg", MinPrice = 200, MaxPrice = 1500 },
            new PriceBenchmark { Category = "Cleaning", Area = "Cape Town", MinPrice = 250, MaxPrice = 1800 },
            new PriceBenchmark { Category = "Cleaning", Area = "Durban", MinPrice = 180, MaxPrice = 1200 },
            new PriceBenchmark { Category = "Cleaning", Area = "Pretoria", MinPrice = 180, MaxPrice = 1200 },
            new PriceBenchmark { Category = "Cleaning", Area = "Soweto", MinPrice = 150, MaxPrice = 800 },

            // Gardening
            new PriceBenchmark { Category = "Gardening", Area = "Johannesburg", MinPrice = 200, MaxPrice = 1500 },
            new PriceBenchmark { Category = "Gardening", Area = "Cape Town", MinPrice = 250, MaxPrice = 1800 },
            new PriceBenchmark { Category = "Gardening", Area = "Durban", MinPrice = 180, MaxPrice = 1200 },
            new PriceBenchmark { Category = "Gardening", Area = "Pretoria", MinPrice = 180, MaxPrice = 1200 },
            new PriceBenchmark { Category = "Gardening", Area = "Soweto", MinPrice = 150, MaxPrice = 800 },

            // Plumbing
            new PriceBenchmark { Category = "Plumbing", Area = "Johannesburg", MinPrice = 300, MaxPrice = 2000 },
            new PriceBenchmark { Category = "Plumbing", Area = "Cape Town", MinPrice = 350, MaxPrice = 2500 },
            new PriceBenchmark { Category = "Plumbing", Area = "Durban", MinPrice = 250, MaxPrice = 1800 },
            new PriceBenchmark { Category = "Plumbing", Area = "Pretoria", MinPrice = 250, MaxPrice = 1800 },
            new PriceBenchmark { Category = "Plumbing", Area = "Soweto", MinPrice = 200, MaxPrice = 1200 },

            // Tutoring
            new PriceBenchmark { Category = "Tutoring", Area = "Johannesburg", MinPrice = 150, MaxPrice = 800 },
            new PriceBenchmark { Category = "Tutoring", Area = "Cape Town", MinPrice = 180, MaxPrice = 900 },
            new PriceBenchmark { Category = "Tutoring", Area = "Durban", MinPrice = 120, MaxPrice = 700 },
            new PriceBenchmark { Category = "Tutoring", Area = "Pretoria", MinPrice = 120, MaxPrice = 700 },
            new PriceBenchmark { Category = "Tutoring", Area = "Soweto", MinPrice = 80, MaxPrice = 400 },
        };

        context.PriceBenchmarks.AddRange(benchmarks);
        await context.SaveChangesAsync();
    }
}
