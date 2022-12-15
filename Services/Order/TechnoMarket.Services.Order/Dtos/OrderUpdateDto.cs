namespace TechnoMarket.Services.Order.Dtos
{
    public class OrderUpdateDto
    {
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string Status { get; set; }
        public AddressDto Address { get; set; }
        public ProductDto Product { get; set; }
    }
}
