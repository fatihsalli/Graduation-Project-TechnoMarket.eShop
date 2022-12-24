namespace TechnoMarket.Web.Models
{
    public class ServiceApiSettings
    {
        public string GatewayBaseUri { get; set; }
        public string IdentityBaseUri { get; set; }
        public string PhotoStockUri { get; set; }
        public ServiceApi Catalog { get; set; }
        public ServiceApi Customer { get; set; }
        public ServiceApi Basket { get; set; }
        public ServiceApi Order { get; set; }
        public ServiceApi PhotoStock { get; set; }

    }

    public class ServiceApi
    {
        public string Path { get; set; }
    }


}
