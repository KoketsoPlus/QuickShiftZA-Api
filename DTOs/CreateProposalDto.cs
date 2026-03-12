using System;

namespace QuickShiftZA.Api.DTOs;

public class CreateProposalDto
{
    public Guid GigId { get; set; }
    public Guid WorkerId { get; set; }
    public decimal ProposedAmount { get; set; }
    public string? Message { get; set; }
}
