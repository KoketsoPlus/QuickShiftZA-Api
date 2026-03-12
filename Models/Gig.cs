using System;

namespace QuickShiftZA.Api.Models;

public class Gig
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid CustomerId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Category { get; set; } = string.Empty;
    public string Area { get; set; } = string.Empty;
    public decimal ProposedPrice { get; set; }
    public string PriceLabel { get; set; } = string.Empty; // fair, too_low, too_high
    public string Status { get; set; } = "open";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public User? Customer { get; set; }
}
