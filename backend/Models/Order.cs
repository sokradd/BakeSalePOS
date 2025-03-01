using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BakeSale.API.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string? Status { get; set; }

        public int SalespersonId { get; set; }

        [ForeignKey("SalespersonId")] public Salesperson Salesperson { get; set; } = null!;
        public List<OrderLine> OrderLines { get; set; } = new();
    }
}