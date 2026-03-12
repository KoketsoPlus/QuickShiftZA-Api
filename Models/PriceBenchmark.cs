using System;

namespace QuickShiftZA.Api.Models;

public class PriceBenchmark
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Category { get; set; } = string.Empty;
    public string Area { get; set; } = string.Empty;
    public decimal MinPrice { get; set; }
    public decimal MaxPrice { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

