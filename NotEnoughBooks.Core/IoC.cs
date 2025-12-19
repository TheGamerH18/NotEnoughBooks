using Microsoft.Extensions.DependencyInjection;
using NotEnoughBooks.Core.UseCases;
using NotEnoughBooks.Core.UseCases.Interfaces;

namespace NotEnoughBooks.Core;

public static class IoC
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddScoped<IRequestNewBookUseCase, RequestNewBookUseCase>();
        services.AddScoped<IGetBooksByUserUseCase, GetBooksByUserUseCase>();
        services.AddScoped<ISaveBookUseCase, SaveBookUseCase>();
        services.AddScoped<IGetBookUseCase, GetBookUseCase>();
        services.AddScoped<ICheckAdminConfigurationUseCase, CheckAdminConfigurationUseCase>();
        services.AddScoped<ISetAdminConfigurationUseCase, SetAdminConfigurationUseCase>();
        services.AddScoped<ISearchUseCase, SearchUseCase>();
        services.AddScoped<IDeleteBookUseCase, DeleteBookUseCase>();
        return services;
    }
}