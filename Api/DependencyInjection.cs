using Api.Common.Errors;
using Api.Common.Mapping;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddCors();
        services.AddSingleton<ProblemDetailsFactory, KeyDetectProblemDetailsFactory>();
        services.AddMappings();
        return services;
    }
}