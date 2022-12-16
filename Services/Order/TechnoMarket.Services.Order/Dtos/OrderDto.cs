namespace TechnoMarket.Services.Order.Dtos
{
    public class OrderDto
    {
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }
        public string FullAddress { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
    }
}
