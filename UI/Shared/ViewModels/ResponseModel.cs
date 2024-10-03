namespace Shared.ViewModels;

public class ResponseModel
{
    public bool Status { get; set; }
    public object? Content { get; set; }

    public ResponseModel() { }

    public ResponseModel(bool status, object? content)
    {
        Status = status;
        Content = content;
    }
}
