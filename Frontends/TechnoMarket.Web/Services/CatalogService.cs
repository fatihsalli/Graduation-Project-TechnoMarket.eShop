using FreeCourse.Web.Helpers;
using FreeCourse.Web.Services.Interfaces;
using TechnoMarket.Shared.Dtos;
using TechnoMarket.Web.Models.Catalog;
using TechnoMarket.Web.Services.Interfaces;

namespace TechnoMarket.Web.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _httpClient;
        private readonly IPhotoStockService _photoStockService;
        private readonly PhotoHelper _photoHelper;

        public CatalogService(HttpClient httpClient, IPhotoStockService photoStockService, PhotoHelper photoHelper)
        {
            _httpClient = httpClient;
            _photoStockService = photoStockService;
            _photoHelper = photoHelper;
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

            //Fotoğrafları Url olarak ekliyoruz. PhotoStockdan istek yapacak şekilde.
            responseSuccess.Data.ForEach(x =>
            {
                x.StockPictureUrl = _photoHelper.GetPhotoStockUrl(x.ImageFile);
            });

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
            var resultPhotoService = await _photoStockService.UploadPhoto(productCreateInput.PhotoFormFile);

            if (resultPhotoService != null)
            {
                productCreateInput.ImageFile = resultPhotoService.Url;
            }

            var response = await _httpClient.PostAsJsonAsync<ProductCreateInput>("products", productCreateInput);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateProductAsync(ProductUpdateInput productUpdateInput)
        {
            var resultPhotoService = await _photoStockService.UploadPhoto(productUpdateInput.PhotoFormFile);

            if (resultPhotoService != null)
            {
                //eskisini silmek için
                await _photoStockService.DeletePhoto(productUpdateInput.ImageFile);
                productUpdateInput.ImageFile = resultPhotoService.Url;
            }

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

        public async Task<bool> CreateCategoryAsync(CategoryCreateInput categoryCreateInput)
        {
            var response = await _httpClient.PostAsJsonAsync<CategoryCreateInput>("categories", categoryCreateInput);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateCategoryAsync(CategoryUpdateInput categoryUpdateInput)
        {
            var response = await _httpClient.PutAsJsonAsync<CategoryUpdateInput>("categories", categoryUpdateInput);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteCategoryAsync(string id)
        {
            var response = await _httpClient.DeleteAsync($"categories/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
