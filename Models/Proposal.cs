using System;

namespace QuickShiftZA.Api.Models;

public class Proposal
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid GigId { get; set; }
    public Guid WorkerId { get; set; }
    public decimal ProposedAmount { get; set; }
    public string? Message { get; set; }
    public string Status { get; set; } = "pending";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Gig? Gig { get; set; }
    public User? Worker { get; set; }
}
