namespace BakeSale.API.DTOs
{
    public class OrderLineDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public ProductDto? Product { get; set; }
    }
}