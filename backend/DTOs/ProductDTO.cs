namespace BakeSale.API.DTOs;

public class ProductDto
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public decimal Cost { get; set; }
}