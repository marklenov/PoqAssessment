using PoqAssessment.Clients.Models;
using PoqAssessment.DTOs;

namespace PoqAssessment.Interfaces;

public interface IProductsService
{
    ProductsResponseDTO GetProductsFiltered(ProductsRequestDTO requestDTO, ClientProducts products);
}
