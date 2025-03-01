namespace BakeSale.API.DTOs;

public class ProductDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal Cost { get; set; }
    public string ProductType { get; set; } = string.Empty;
    public int StartingQuantity { get; set; }
    public int CurrentQuantity { get; set; }
}