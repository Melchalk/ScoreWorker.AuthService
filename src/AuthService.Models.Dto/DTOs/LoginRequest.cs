namespace AuthService.Models.Dto.DTOs;

public record LoginRequest
{
    public required string Login { get; set; }
    public required string Password { get; set; }
}