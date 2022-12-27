using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoMarket.Shared.Messages
{
    //Command Örneği 
    //TODO:Devam edilecek Udemy ile birlikte
    public class CreateOrderMessageCommand
    {
        public string CustomerId { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }
        public AddressDto Address { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
    }

    public class OrderItemDto
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

    public class AddressDto
    {
        public string AddressLine { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int CityCode { get; set; }
    }

}
