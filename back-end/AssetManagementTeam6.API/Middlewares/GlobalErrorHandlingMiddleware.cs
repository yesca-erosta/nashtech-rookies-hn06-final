﻿using AssetManagementTeam6.API.ErrorHandling;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Text.Json;

namespace AssetManagementTeam6.API.Middlewares
{
    [ExcludeFromCodeCoverage]
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExecptionAsync(context, ex);
            }
        }

        private static Task HandleExecptionAsync(HttpContext context, Exception ex)
        {
            string? expectedResult;
            string? message;
            HttpStatusCode statusCode;

            switch (ex)
            {
                case MyCustomException myCustomException:
                    statusCode = myCustomException.StatusCode;
                    message = myCustomException.Message;
                    expectedResult = JsonSerializer.Serialize(new { errors = myCustomException.Error, message, stackTrace = (string)null! });
                    break;

                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    message = ex.Message;
                    expectedResult = JsonSerializer.Serialize(new { errors = (string)null!, message, stackTrace = ex.StackTrace });
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(expectedResult);
        }
    }
}
