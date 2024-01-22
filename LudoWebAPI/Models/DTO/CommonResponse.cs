namespace LudoWebAPI.Models.DTO
{
    public class CommonResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public CommonResponse(bool success, string message, T data)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        public static CommonResponse<T> SuccessReq(T data, string message = "Operation successful.")
        {
            return new CommonResponse<T>(true, message, data);
        }

        public static CommonResponse<T> ErrorReq(string message = "An error occurred.", T data = default)
        {
            return new CommonResponse<T>(false, message, data);
        }
    }
}
