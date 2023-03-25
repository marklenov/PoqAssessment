using Microsoft.Extensions.Options;
using PoqAssessment.Clients.Interfaces;
using PoqAssessment.DTOs;
using PoqAssessment.Exceptions;
using PoqAssessment.Interfaces;
using PoqAssessment.Options;

namespace PoqAssessment.Services;

public class ServiceHandler : IServiceHandler
{
    private readonly IProductsClientService _productsClientService;
    private readonly IProductsService _productsService;
    private readonly ILogger<ServiceHandler> _logger;
    private readonly ProductsClientOptions _options;

    public ServiceHandler(IProductsService productsService, IProductsClientService productsClientService,
        ILogger<ServiceHandler> logger, IOptions<ProductsClientOptions> options)
    {
        _productsService = productsService;
        _productsClientService = productsClientService;
        _logger = logger;
        _options = options.Value;
    }

    public async Task<ProductsResponseDTO> HandleProducts(ProductsRequestDTO requestDTO, CancellationToken cancellationToken)
    {
        var productsFromClient = await _productsClientService.FetchProductsAsync(_options.Url, cancellationToken);
        if (productsFromClient == null)
        {
            _logger.LogError("Error appeared when calling external API");
            throw new AppException("Error on external API");
        }

        var products = _productsService.GetProductsFiltered(requestDTO, productsFromClient);

        return products;
    }
}
