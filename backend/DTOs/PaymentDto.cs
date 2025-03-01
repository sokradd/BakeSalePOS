namespace BakeSale.API.DTOs
{
    public class PaymentDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public decimal CashPaid { get; set; }
        public decimal ChangeReturned { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}