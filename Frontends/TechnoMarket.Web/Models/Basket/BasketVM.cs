namespace TechnoMarket.Web.Models.Basket
{
    public class BasketVM
    {
        public BasketVM()
        {
            BasketItems=new List<BasketItemVM>();
        }

        public string CustomerId { get; set; }
        public List<BasketItemVM> BasketItems { get; set; }
        public decimal TotalPrice
        {
            get => BasketItems.Sum(x => x.Price * x.Quantity);
        }
    }
}
