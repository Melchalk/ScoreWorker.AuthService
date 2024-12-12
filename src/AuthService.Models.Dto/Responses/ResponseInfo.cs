namespace AuthService.Models.Dto.Responses;

public class ResponseInfo<T>
{
    public int Status { get; set; }
    public string? ErrorMessage { get; set; }
    public T? Body { get; set; }
}
