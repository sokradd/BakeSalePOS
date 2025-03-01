using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BakeSale.API.Models;

public class Payment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required] public int OrderId { get; set; }

    public Order? Order { get; set; }

    [Required] public decimal CashPaid { get; set; }

    [Required] public decimal ChangeReturned { get; set; }

    public DateTime PaymentDate { get; set; }
}