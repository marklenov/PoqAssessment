namespace PoqAssessment.DTOs;

public class ProductsRequestDTO
{
    public int? MinPrice { get; set; }
    public int? MaxPrice { get; set; }
    public string Size { get; set; } = string.Empty;
    public string Highlight { get; set; } = string.Empty;
}
