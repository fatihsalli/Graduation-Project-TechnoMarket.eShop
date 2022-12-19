using TechnoMarket.Shared.Dtos;
using TechnoMarket.Web.Models.Catalog;
using TechnoMarket.Web.Services.Interfaces;

namespace TechnoMarket.Web.Services
{
    public class CatalogService:ICatalogService
    {
        private readonly HttpClient _httpClient;
        public CatalogService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ProductVM>> GetAllProductsAsync()
        {
            var response = await _httpClient.GetAsync("products");

            if (!response.IsSuccessStatusCode)
            {
                //Exception-loglama
                return null;
            }

            var responseSuccess = await response.Content.ReadFromJsonAsync<CustomResponseDto<List<ProductVM>>>();

            return responseSuccess.Data;
        }



    }
}
