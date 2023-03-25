namespace PoqAssessment.DTOs;

public class FilterObjectResponseDTO
{
    public int? MinPrice { get; set; }
    public int? MaxPrice { get; set; }
    public IEnumerable<string> Sizes { get; set; }
    public IEnumerable<string> CommonWords { get; set; }
}
