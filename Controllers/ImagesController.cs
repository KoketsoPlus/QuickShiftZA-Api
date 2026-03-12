using Microsoft.AspNetCore.Mvc;
using QuickShiftZA.Api.Services;

namespace QuickShiftZA.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ImagesController : ControllerBase
{
    private readonly ObsService _obsService;

    public ImagesController(ObsService obsService)
    {
        _obsService = obsService;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> Upload(IFormFile file, [FromQuery] string folder = "work-images")
    {
        if (file == null || file.Length == 0)
            return BadRequest("No file uploaded.");

        if (file.Length > 5 * 1024 * 1024)
            return BadRequest("File size cannot exceed 5MB.");

        var allowed = new[] { ".jpg", ".jpeg", ".png", ".webp" };
        var ext = Path.GetExtension(file.FileName).ToLower();
        if (!allowed.Contains(ext))
            return BadRequest("Only jpg, jpeg, png and webp files are allowed.");

        var url = await _obsService.UploadImageAsync(file, folder);
        return Ok(new { url });
    }
}