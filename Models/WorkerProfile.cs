using System;
namespace QuickShiftZA.Api.Models;

public class WorkerProfile
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public string SkillCategory { get; set; } = string.Empty;
    public string? Bio { get; set; }
    public decimal AverageRating { get; set; } = 0;
    public decimal HourlyRate { get; set; } = 0;
    public List<string> PortfolioImages { get; set; } = new();
    public User? User { get; set; }
}