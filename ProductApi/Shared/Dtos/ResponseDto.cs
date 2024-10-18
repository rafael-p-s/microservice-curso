namespace Shared.Dtos;

public class ResponseDto
{
    public bool Status { get; set; }
    public object? Content { get; set; }

    public ResponseDto() { }

    public ResponseDto(bool status, object? content)
    {
        Status = status;
        Content = content;
    }
}
