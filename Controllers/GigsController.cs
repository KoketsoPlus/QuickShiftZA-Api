using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuickShiftZA.Api.Data;
using QuickShiftZA.Api.DTOs;
using QuickShiftZA.Api.Models;
using QuickShiftZA.Api.Services;

namespace QuickShiftZA.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GigsController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly PriceCheckerService _priceChecker;

    public GigsController(AppDbContext context, PriceCheckerService priceChecker)
    {
        _context = context;
        _priceChecker = priceChecker;
    }

    [HttpGet]
    public async Task<IActionResult> GetGigs([FromQuery] string? area)
    {
        var query = _context.Gigs.AsQueryable();

        if (!string.IsNullOrWhiteSpace(area))
            query = query.Where(g => g.Area == area);

        var gigs = await query.OrderByDescending(g => g.CreatedAt).ToListAsync();
        return Ok(gigs);
    }

    [HttpPost]
    public async Task<IActionResult> CreateGig(CreateGigDto dto)
    {
        var benchmark = await _context.PriceBenchmarks
            .FirstOrDefaultAsync(b => b.Category == dto.Category && b.Area == dto.Area);

        if (benchmark is null)
            return BadRequest("No benchmark found for this category and area.");

        var label = _priceChecker.CheckPrice(dto.ProposedPrice, benchmark);

        var gig = new Gig
        {
            CustomerId = dto.CustomerId,
            Title = dto.Title,
            Description = dto.Description,
            Category = dto.Category,
            Area = dto.Area,
            ProposedPrice = dto.ProposedPrice,
            PriceLabel = label
        };

        _context.Gigs.Add(gig);
        await _context.SaveChangesAsync();

        return Ok(gig);
    }
}