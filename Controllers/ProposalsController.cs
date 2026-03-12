using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuickShiftZA.Api.Data;
using QuickShiftZA.Api.DTOs;
using QuickShiftZA.Api.Models;

namespace QuickShiftZA.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProposalsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProposalsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProposal(CreateProposalDto dto)
    {
        var proposal = new Proposal
        {
            GigId = dto.GigId,
            WorkerId = dto.WorkerId,
            ProposedAmount = dto.ProposedAmount,
            Message = dto.Message
        };

        _context.Proposals.Add(proposal);
        await _context.SaveChangesAsync();

        return Ok(proposal);
    }

    [HttpGet("gig/{gigId}")]
    public async Task<IActionResult> GetProposalsForGig(Guid gigId)
    {
        var proposals = await _context.Proposals
            .Where(p => p.GigId == gigId)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();

        return Ok(proposals);
    }
}
