using TechnoMarket.Shared.Dtos;
using TechnoMarket.Web.Areas.Admin.Models.Products;
using TechnoMarket.Web.Models.Catalog;
using TechnoMarket.Web.Services.Interfaces;

namespace TechnoMarket.Web.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _httpClient;
        public CatalogService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        //=> For Product
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

        public async Task<bool> CreateProductAsync(ProductCreateInput productCreateInput)
        {
            var response = await _httpClient.PostAsJsonAsync<ProductCreateInput>("products", productCreateInput);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateProductAsync(ProductUpdateInput productUpdateInput)
        {
            var response = await _httpClient.PutAsJsonAsync<ProductUpdateInput>("products", productUpdateInput);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteProductAsync(string id)
        {
            var response = await _httpClient.DeleteAsync($"products/{id}");
            return response.IsSuccessStatusCode;
        }

        //=> For Category
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

        public async Task<CategoryVM> GetCategoryByIdAsync(string id)
        {
            var response = await _httpClient.GetAsync($"categories/{id}");

            if (!response.IsSuccessStatusCode)
            {
                //Exception-loglama
                return null;
            }

            var responseSuccess = await response.Content.ReadFromJsonAsync<CustomResponseDto<CategoryVM>>();

            return responseSuccess.Data;
        }



    }
}
