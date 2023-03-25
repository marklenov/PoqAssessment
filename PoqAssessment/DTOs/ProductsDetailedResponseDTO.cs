namespace PoqAssessment.DTOs;

public class ProductsDetailedResponseDTO
{
    public string Title { get; set; }
    public int Price { get; set; }
    public IEnumerable<string> Sizes { get; set; }
    public string Description { get; set; }
}
