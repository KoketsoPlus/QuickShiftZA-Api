using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuickShiftZA.Api.Data;
using QuickShiftZA.Api.DTOs;
using QuickShiftZA.Api.Services;

namespace QuickShiftZA.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorkerProfilesController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly ObsService _obsService;

    public WorkerProfilesController(AppDbContext context, ObsService obsService)
    {
        _context = context;
        _obsService = obsService;
    }

    // GET all workers (for Providers page)
    [HttpGet]
    public async Task<IActionResult> GetWorkers()
    {
        var workers = await _context.WorkerProfiles
            .Include(w => w.User)
            .Select(w => new
            {
                w.Id,
                w.SkillCategory,
                w.Bio,
                w.AverageRating,
                w.HourlyRate,
                w.PortfolioImages,
                Name = w.User!.FullName,
                Location = w.User.Area,
                Avatar = w.User.ProfileImageUrl
            })
            .ToListAsync();

        return Ok(workers);
    }

    // GET single worker profile
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetWorker(Guid userId)
    {
        var worker = await _context.WorkerProfiles
            .Include(w => w.User)
            .FirstOrDefaultAsync(w => w.UserId == userId);

        if (worker is null) return NotFound();

        return Ok(new
        {
            worker.Id,
            worker.UserId,
            worker.SkillCategory,
            worker.Bio,
            worker.AverageRating,
            worker.HourlyRate,
            worker.PortfolioImages,
            Name = worker.User!.FullName,
            Location = worker.User.Area,
            Avatar = worker.User.ProfileImageUrl
        });
    }

    // UPDATE worker profile
    [HttpPut("{userId}")]
    public async Task<IActionResult> UpdateProfile(Guid userId, UpdateWorkerProfileDto dto)
    {
        var worker = await _context.WorkerProfiles
            .FirstOrDefaultAsync(w => w.UserId == userId);

        if (worker is null) return NotFound();

        worker.SkillCategory = dto.SkillCategory;
        worker.Bio = dto.Bio;
        worker.HourlyRate = dto.HourlyRate;

        await _context.SaveChangesAsync();
        return Ok(worker);
    }

    // UPLOAD portfolio image
    [HttpPost("{userId}/portfolio")]
    public async Task<IActionResult> UploadPortfolioImage(Guid userId, IFormFile file)
    {
        var worker = await _context.WorkerProfiles
            .FirstOrDefaultAsync(w => w.UserId == userId);

        if (worker is null) return NotFound();

        var url = await _obsService.UploadImageAsync(file, "portfolio");
        worker.PortfolioImages.Add(url);

        await _context.SaveChangesAsync();
        return Ok(new { url });
    }

    // UPLOAD profile picture
    [HttpPost("{userId}/avatar")]
    public async Task<IActionResult> UploadAvatar(Guid userId, IFormFile file)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user is null) return NotFound();

        var url = await _obsService.UploadImageAsync(file, "avatars");
        user.ProfileImageUrl = url;

        await _context.SaveChangesAsync();
        return Ok(new { url });
    }
}
