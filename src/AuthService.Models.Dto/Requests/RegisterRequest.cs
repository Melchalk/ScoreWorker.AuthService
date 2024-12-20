namespace AuthService.Models.Dto.Requests;

public class RegisterRequest
{
    public required string Login { get; set; }

    public required string Password { get; set; }

    public required string Phone { get; set; }
}