using TechnoMarket.Shared.Dtos;
using TechnoMarket.Web.Areas.Admin.Models;
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

        public async Task<ProductVM> GetProductByIdAsync(string id)
        {
            var response = await _httpClient.GetAsync($"products/{id}");

            if (!response.IsSuccessStatusCode)
            {
                //Exception-loglama
                return null;
            }

            var responseSuccess = await response.Content.ReadFromJsonAsync<CustomResponseDto<ProductVM>>();

            return responseSuccess.Data;
        }

        public async Task<bool> CreateCourseAsync(ProductCreateInput productCreateInput)
        {
            var response = await _httpClient.PostAsJsonAsync<ProductCreateInput>("products", productCreateInput);
            return response.IsSuccessStatusCode;
        }


        public async Task<List<CategoryVM>> GetAllCategoriesAsync()
        {
            var response = await _httpClient.GetAsync("categories");

            if (!response.IsSuccessStatusCode)
            {
                //Exception-loglama
                return null;
            }

            var responseSuccess = await response.Content.ReadFromJsonAsync<CustomResponseDto<List<CategoryVM>>>();

            return responseSuccess.Data;
        }



    }
}
