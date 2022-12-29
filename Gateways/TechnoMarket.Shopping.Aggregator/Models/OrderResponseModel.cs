namespace TechnoMarket.Shopping.Aggregator.Models
{
    public class OrderResponseModel
    {
        public OrderResponseModel()
        {
            OrderItems = new List<OrderItemModel>();
        }
        public string CustomerId { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }
        public AddressModel Address { get; set; }
        public List<OrderItemModel> OrderItems { get; set; }
    }

    public class AddressModel
    {
        public string AddressLine { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int CityCode { get; set; }
    }

}
