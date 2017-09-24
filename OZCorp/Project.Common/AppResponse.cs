using System.Collections.Generic;
using System.Linq;

namespace ProjectStart.Common
{
    public class Response
    {
        private bool _success;
        public Response(bool success = true, string message = null, IDictionary<string, string> errors = null)
        {
            Message = message;
            Success = success;
            Errors = errors;
        }

        public bool Success
        {
            get => _success && (Errors == null || !Errors.Any());
            set => _success = value;
        }

        public bool Error => !Success;
        public IDictionary<string, string> Errors { get; set; }
        public string Message { get; set; }
    }
    public class Response<T> : Response
    {
        public Response(T data, bool success = true, string message = null, IDictionary<string, string> errors = null) : base(success, message, errors)
        {
            Data = data;
        }
        public T Data { get; set; }
    }
    public static class ResponseExtension
    {
        public static Response Create(this Response response)
            => new Response();
        public static Response Create(this Response response, bool success = true, string message = null, IDictionary<string, string> errors = null)
            => new Response(success, message, errors);
        public static Response<T> ToResponse<T>(this T data, bool success = true, string message = null, IDictionary<string, string> errors = null)
            => new Response<T>(data, success, message, errors);
    }
}
