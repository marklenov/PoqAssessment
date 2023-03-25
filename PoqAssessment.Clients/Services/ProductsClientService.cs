using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PoqAssessment.Clients.Interfaces;
using PoqAssessment.Clients.Models;
using System.Net.Http.Json;

namespace PoqAssessment.Clients.Services;

public class ProductsClientService : IProductsClientService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<ProductsClientService> _logger;

    public ProductsClientService(HttpClient httpClient, ILogger<ProductsClientService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<ClientProducts> FetchProductsAsync(string url, CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetFromJsonAsync<ClientProducts>
            (url, cancellationToken);

        if (response is null)
        {
            _logger.LogInformation("External API returned error");
            throw new ApplicationException("External API returned error");
        }

        _logger.LogInformation(JsonConvert.SerializeObject(response));

        return response;
    }

}
