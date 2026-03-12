using System;

namespace QuickShiftZA.Api.Models;

public class Rating
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid GigId { get; set; }
    public Guid CustomerId { get; set; }
    public Guid WorkerId { get; set; }
    public int Score { get; set; }
    public string? Review { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
