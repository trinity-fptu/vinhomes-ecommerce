namespace Application.ResponseModels;

public class FailResponseModel
{
    public int Status { get; set; }
    public string Message { get; set; }
    public object Errors { get; set; }
}