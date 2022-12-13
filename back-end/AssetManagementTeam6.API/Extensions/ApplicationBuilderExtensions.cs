using AssetManagementTeam6.API.Middlewares;
using System.Diagnostics.CodeAnalysis;

namespace AssetManagementTeam6.API.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder AddGlobalErrorHandler(this IApplicationBuilder applicationBuilder)
            => applicationBuilder.UseMiddleware<GlobalErrorHandlingMiddleware>();
    }
}
