using FluentValidation.Results;
using System.Text;

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

        public static ResultResponse<T> Failure(int statusCode, List<ValidationFailure> errors)
        {
            StringBuilder sb = new();
            errors.ForEach(e => { sb.AppendLine(e.ErrorMessage); });
            return new()
            {
                Message = sb.ToString(),
                StatusCode = statusCode,
                Success = false,
            };
        }
    }
}
