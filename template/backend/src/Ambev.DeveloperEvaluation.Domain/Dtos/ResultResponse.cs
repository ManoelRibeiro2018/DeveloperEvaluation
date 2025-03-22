namespace Ambev.DeveloperEvaluation.Domain.Dtos
{
    public class ResultResponse<T> where T : class
    {
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public T Payload { get; set; }
        public bool Success { get; set; }

        public static ResultResponse<T> Successful(T payload, int statusCode, string message) => new()
        {
            Message = message,
            StatusCode = statusCode,
            Payload = payload,
            Success = true
        };

        public static ResultResponse<T> Failure( int statusCode, string message) => new()
        {
            Message = message,
            StatusCode = statusCode,
            Success = false
        };
    }
}
