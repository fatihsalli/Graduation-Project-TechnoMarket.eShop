using TechnoMarket.Services.Customer.Settings.Interfaces;

namespace TechnoMarket.Services.Customer.Settings
{
    public class CustomerDatabaseSettings:ICustomerDatabaseSettings
    {
        public string ConnectionString { get; set; }
    }
}
