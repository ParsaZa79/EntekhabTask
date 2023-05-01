namespace EntekhabSalary.WebApi.Contracts.Responses;

public class ResponseBase<T>
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }
    
    public static ResponseBase<T> CreateSuccessResponse(T data, string message = "Successful")
    {
        return new ResponseBase<T>
        {
            IsSuccess = true,
            Message = message,
            Data = data
        };
    }

    public static ResponseBase<T> CreateErrorResponse(string message, T data = default(T))
    {
        return new ResponseBase<T>
        {
            IsSuccess = false,
            Message = message,
            Data = data
        };
    }
}
