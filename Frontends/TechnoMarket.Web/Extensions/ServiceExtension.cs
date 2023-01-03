using FreeCourse.Web.Services;
using FreeCourse.Web.Services.Interfaces;
using TechnoMarket.Shared.CommonLogging;
using TechnoMarket.Web.Models;
using TechnoMarket.Web.Services;
using TechnoMarket.Web.Services.Interfaces;

namespace TechnoMarket.Web.Extensions
{
    public static class ServiceExtension
    {
        public static void AddHttpClientServices(this IServiceCollection services, IConfiguration Configuration)
        {
            //Options pattern ile oluşturduğumuz ServiceApiSettings classına ulaştık. Buradan path'i alarak ilgili servise ekleyeceğiz.
            var serviceApisettings = Configuration.GetSection(nameof(ServiceApiSettings)).Get<ServiceApiSettings>();

            services.AddTransient<LoggingDelegatingHandler>();

            //Catalog.Api için => HttpClient ile nesne türeterek yeni ürettiğimiz path üzerinden istek yapacağız.
            services.AddHttpClient<ICatalogService, CatalogService>(opt =>
            {
                opt.BaseAddress = new Uri($"{serviceApisettings.GatewayBaseUri}/{serviceApisettings.Catalog.Path}");
            }).AddHttpMessageHandler<LoggingDelegatingHandler>();

            //PhotoStock.Api için => HttpClient ile nesne türeterek yeni ürettiğimiz path üzerinden istek yapacağız.
            services.AddHttpClient<IPhotoStockService, PhotoStockService>(opt =>
            {
                opt.BaseAddress = new Uri($"{serviceApisettings.GatewayBaseUri}/{serviceApisettings.PhotoStock.Path}");
            }).AddHttpMessageHandler<LoggingDelegatingHandler>();

            //Customer.Api için => HttpClient ile nesne türeterek yeni ürettiğimiz path üzerinden istek yapacağız.
            services.AddHttpClient<ICustomerService, CustomerService>(opt =>
            {
                opt.BaseAddress = new Uri($"{serviceApisettings.GatewayBaseUri}/{serviceApisettings.Customer.Path}");
            }).AddHttpMessageHandler<LoggingDelegatingHandler>();

            //Order.Api için => HttpClient ile nesne türeterek yeni ürettiğimiz path üzerinden istek yapacağız.
            services.AddHttpClient<IOrderService, OrderService>(opt =>
            {
                opt.BaseAddress = new Uri($"{serviceApisettings.GatewayBaseUri}/{serviceApisettings.Order.Path}");
            }).AddHttpMessageHandler<LoggingDelegatingHandler>();

            //Basket.Api için => HttpClient ile nesne türeterek yeni ürettiğimiz path üzerinden istek yapacağız.
            services.AddHttpClient<IBasketService, BasketService>(opt =>
            {
                opt.BaseAddress = new Uri($"{serviceApisettings.GatewayBaseUri}/{serviceApisettings.Basket.Path}");
            }).AddHttpMessageHandler<LoggingDelegatingHandler>();


        }
    }
}
