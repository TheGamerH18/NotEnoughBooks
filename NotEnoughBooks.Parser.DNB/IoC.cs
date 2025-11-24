using Microsoft.Extensions.DependencyInjection;
using NotEnoughBooks.Core.Ports;

namespace NotEnoughBooks.Parser.DNB;

public static class IoC
{
    public static IServiceCollection AddDnbParser(this IServiceCollection services)
    {
        services.AddScoped<IGetBookByQueryPort, Parser>();
        return services;
    }
}