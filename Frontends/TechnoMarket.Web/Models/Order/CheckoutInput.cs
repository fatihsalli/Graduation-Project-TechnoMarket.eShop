using System.ComponentModel.DataAnnotations;

namespace TechnoMarket.Web.Models.Order
{
    public class CheckoutInput
    {
        [Display(Name = "AddressLine")]
        public string AddressLine { get; set; }
        [Display(Name = "City")]
        public string City { get; set; }
        [Display(Name = "Country")]
        public string Country { get; set; }
        [Display(Name = "CityCode")]
        public int CityCode { get; set; }

        //Kart bilgileri => Payment
        [Display(Name = "CardName")]
        public string CardName { get; set; }

        [Display(Name = "CardNumber")]
        public string CardNumber { get; set; }

        [Display(Name = "Expiration")]
        public string Expiration { get; set; }

        [Display(Name = "CVV/CVC2")]
        public string CVV { get; set; }
    }
}
