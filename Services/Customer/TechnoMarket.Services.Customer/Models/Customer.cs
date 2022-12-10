using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechnoMarket.Services.Customer.Models
{
    //TODO: Id konusunda PostgreSql serial olarak kullanıyor bunun tipi int. Diğer servisler ise string. UUIDV4 uygulanmak istendiği için string değer verildi. Program tarafında Id verilecek. PostgreSql tarafında hazır bir tip olarak "uuid" var ancak string tip kabul etmiyor Guid istiyor ve generate esnasında kontrollü üretebilmek adına program tarafında Id belirledik.
    public class Customer
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
