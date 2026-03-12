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
        // Try exact area + category match first
        var benchmark = await _context.PriceBenchmarks
            .FirstOrDefaultAsync(b => b.Category.ToLower() == dto.Category.ToLower() &&
                dto.Area.ToLower().Contains(b.Area.ToLower()));

        // Fallback: any benchmark for this category
        if (benchmark is null)
            benchmark = await _context.PriceBenchmarks
                .FirstOrDefaultAsync(b => b.Category.ToLower() == dto.Category.ToLower());

        // Last resort: use default benchmark
        if (benchmark is null)
            benchmark = new PriceBenchmark
            {
                Category = dto.Category,
                Area = dto.Area,
                MinPrice = 100,
                MaxPrice = 5000
            };

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

    [HttpGet("{id}")]
    public async Task<IActionResult> GetGig(Guid id)
    {
        var gig = await _context.Gigs
            .Include(g => g.Customer)
            .FirstOrDefaultAsync(g => g.Id == id);

        if (gig is null)
            return NotFound();

        return Ok(gig);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGig(Guid id)
    {
        var gig = await _context.Gigs.FindAsync(id);
        if (gig is null)
            return NotFound();

        _context.Gigs.Remove(gig);
        await _context.SaveChangesAsync();
        return Ok("Gig deleted successfully.");
    }
}