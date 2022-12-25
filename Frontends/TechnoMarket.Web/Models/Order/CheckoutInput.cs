using System.ComponentModel.DataAnnotations;

namespace TechnoMarket.Web.Models.Order
{
    public class CheckoutInput
    {
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Address Line")]
        public string AddressLine { get; set; }
        [Display(Name = "City")]
        public string City { get; set; }
        [Display(Name = "Country")]
        public string Country { get; set; }
        [Display(Name = "City Code")]
        public int CityCode { get; set; }

        //Kart bilgileri => Payment
        [Display(Name = "Card Name")]
        public string CardName { get; set; }

        [Display(Name = "Card Number")]
        public string CardNumber { get; set; }

        [Display(Name = "Expiration")]
        public string Expiration { get; set; }

        [Display(Name = "CVV/CVC2")]
        public string CVV { get; set; }
    }
}
