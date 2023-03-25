using Microsoft.AspNetCore.Mvc;
using PoqAssessment.DTOs;
using PoqAssessment.Exceptions;
using PoqAssessment.Interfaces;

namespace PoqAssessment.Controllers;

/// <summary>
/// Products request
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IServiceHandler _serviceHandler;
    private readonly ILogger<ProductsController> _logger;

    public ProductsController(ILogger<ProductsController> logger, IServiceHandler serviceHandler)
    {
        _logger = logger;
        _serviceHandler = serviceHandler;
    }

    /// <summary>
    /// Get products filtered by multiple filters
    /// </summary>
    /// <param name="minPrice">minimal price</param>
    /// <param name="maxPrice">maximum price</param>
    /// <param name="size">size of the product</param>
    /// <param name="highlight">words to be filtered and highlighted</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of products</returns>
    /// <exception cref="Exception"></exception>
    [HttpGet(Name = "GetProducts")]
    public async Task<IActionResult> Get([FromQuery] ProductsRequestDTO request, CancellationToken cancellationToken)
    {
        var serviceResponse = await _serviceHandler.HandleProducts(request, cancellationToken);
        if (serviceResponse is null)
        {
            _logger.LogError("GetProducts returned error");
            throw new AppException("GetProducts returned error");
        }

        return Ok(serviceResponse);
    }
}
