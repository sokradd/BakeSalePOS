using System;
using System.Collections.Generic;

namespace BakeSale.API.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = string.Empty;
        public int SalespersonId { get; set; }
        public SalespersonDto? Salesperson { get; set; }
        public List<OrderLineDto> OrderLines { get; set; } = new();
    }
}