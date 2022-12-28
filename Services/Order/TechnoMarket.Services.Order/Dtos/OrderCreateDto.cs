namespace TechnoMarket.Services.Order.Dtos
{
    public class OrderCreateDto
    {
        public OrderCreateDto()
        {
            OrderItems = new List<OrderItemDto>();
        }
        public string CustomerId { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }
        public AddressDto Address { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }

    }
}
