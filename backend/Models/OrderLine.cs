using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BakeSale.API.Models
{
    public class OrderLine
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int OrderId { get; set; } 

        [ForeignKey("OrderId")]
        public Order Order { get; set; } = null!;

        [Required]
        public int ProductId { get; set; } 

        [ForeignKey("ProductId")]
        public Product Product { get; set; } = null!;

        [Required]
        public int Quantity { get; set; }
    }
}