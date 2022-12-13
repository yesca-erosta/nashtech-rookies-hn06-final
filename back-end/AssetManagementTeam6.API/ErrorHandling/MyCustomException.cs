using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace AssetManagementTeam6.API.ErrorHandling
{
    [ExcludeFromCodeCoverage]
    public class MyCustomException: Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public object Error { get; set; } = null!;

        public MyCustomException(string? message): base(message)
        {
            StatusCode = HttpStatusCode.BadRequest;
        }
        public MyCustomException(string? message, HttpStatusCode statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
        public MyCustomException(HttpStatusCode statusCode, string? message = null) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
