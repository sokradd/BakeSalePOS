using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BakeSale.API.Models;

public class Salesperson
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    public required string Name { get; set; }
    
    public List<Order> Orders { get; set; } = new();
}