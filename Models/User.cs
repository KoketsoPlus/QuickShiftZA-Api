
using System;
namespace QuickShiftZA.Api.Models;
public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty; // customer or worker
    public string? Area { get; set; }
    public string? ProfileImageUrl { get; set; } // for OBS later
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}