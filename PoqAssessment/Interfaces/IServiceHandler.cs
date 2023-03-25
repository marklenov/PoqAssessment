using PoqAssessment.DTOs;

namespace PoqAssessment.Interfaces;

public interface IServiceHandler
{
    Task<ProductsResponseDTO> HandleProducts(ProductsRequestDTO requestDTO, CancellationToken cancellationToken);
}
