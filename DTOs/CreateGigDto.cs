using System;

namespace QuickShiftZA.Api.DTOs;

public class CreateGigDto
{
    public Guid CustomerId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Category { get; set; } = string.Empty;
    public string Area { get; set; } = string.Empty;
    public decimal ProposedPrice { get; set; }
}
