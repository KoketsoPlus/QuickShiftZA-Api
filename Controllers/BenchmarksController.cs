using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuickShiftZA.Api.Data;

namespace QuickShiftZA.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BenchmarksController : ControllerBase
{
    private readonly AppDbContext _context;

    public BenchmarksController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetBenchmark([FromQuery] string category, [FromQuery] string area)
    {
        var benchmark = await _context.PriceBenchmarks
            .FirstOrDefaultAsync(b => b.Category == category && b.Area == area);

        if (benchmark is null)
            return NotFound();

        return Ok(benchmark);
    }
}
