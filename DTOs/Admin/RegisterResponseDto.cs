﻿namespace TrainingCenterAPI.DTOs.Auth;
public class RegisterResponseDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public DateTime CreatedAt { get; set; }
}
