using BakeSale.API.Models;

namespace BakeSale.API.DTOs;

public class OrderDto
{
    public int Id { get; set; }
    
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
}