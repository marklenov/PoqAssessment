namespace PoqAssessment.DTOs;

public class ProductsResponseDTO
{
    public List<ProductsDetailedResponseDTO> Products { get; set; }  = new List<ProductsDetailedResponseDTO>();
    public FilterObjectResponseDTO FilterObjects { get; set; }
    
}



