namespace BakeSale.API.DTOs;

public class SecondHandItemDto
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public decimal Cost { get; set; }
}