using BakeSale.API.Models;

namespace BakeSale.API.DTOs;

public class OrderDto
{
    public int Id { get; set; }
    
    public DateTime OrderDate { get; set; }
    public List<Product> Products { get; set; } = new List<Product>();
    public decimal TotalAmount { get; set; }
}