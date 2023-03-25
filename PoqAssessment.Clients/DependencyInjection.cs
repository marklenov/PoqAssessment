using Microsoft.Extensions.DependencyInjection;
using PoqAssessment.Clients.Interfaces;
using PoqAssessment.Clients.Services;

namespace PoqAssessment.Clients;

public static class DependencyInjection
{
    public static IServiceCollection AddProductsClientDI(this IServiceCollection services)
    {
        services.AddHttpClient<IProductsClientService, ProductsClientService>();

        return services;
    }
}
