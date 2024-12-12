namespace AuthService.Models.Dto.DTOs;

public record RefreshRequest
{
    public required string RefreshToken { get; set; }
}