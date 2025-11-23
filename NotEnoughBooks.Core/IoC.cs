using Microsoft.Extensions.DependencyInjection;
using NotEnoughBooks.Core.UseCases;
using NotEnoughBooks.Core.UseCases.Interfaces;

namespace NotEnoughBooks.Core;

public static class IoC
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddScoped<IRequestBookUseCase, RequestBookUseCase>();
        return services;
    }
}