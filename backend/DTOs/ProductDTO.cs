namespace BakeSale.API.DTOs;

public class ProductDto
{
    public required string Title { get; set; }
    public decimal Cost { get; set; }
    
    public required string ProductType { get; set; }
}