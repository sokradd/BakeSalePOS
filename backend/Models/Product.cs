using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BakeSale.API.Models;

public class Product
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public required string Title { get; set; }
    public decimal Cost { get; set; }
    public int StartingQuantity { get; set; }
    public int CurrentQuantity { get; set; }
}