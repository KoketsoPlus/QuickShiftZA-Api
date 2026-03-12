using System;

namespace QuickShiftZA.Api.DTOs;

public class UpdateWorkerProfileDto
{
    public string SkillCategory { get; set; } = string.Empty;
    public string? Bio { get; set; }
    public decimal HourlyRate { get; set; }
}
