using BakeSale.API.Models;

namespace BakeSale.API.DTOs;

public class OrderDto
{
    public int Id { get; set; }

    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public required string Status { get; set; }

    public int SalespersonId { get; set; }
        
    public List<OrderLine> OrderLines { get; set; } 
}