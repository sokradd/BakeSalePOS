using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BakeSale.API.Models;

public class Order
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public DateTime OrderDate { get; set; }
    public List<Product> Products { get; set; } = new List<Product>();
    public decimal TotalAmount { get; set; }
}