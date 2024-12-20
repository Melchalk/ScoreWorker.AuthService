namespace AuthService.Models.Dto.Requests;

public record RefreshRequest
{
    public required string RefreshToken { get; set; }
}