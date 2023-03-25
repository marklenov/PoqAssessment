using AutoMapper;
using PoqAssessment.Clients.Models;
using PoqAssessment.DTOs;

namespace PoqAssessment.Profiles;

public class ProductsProfile : Profile
{
	public ProductsProfile()
	{
        CreateMap<ProductsDetails, ProductsDetailedResponseDTO>()
            .ReverseMap();
    }
}
