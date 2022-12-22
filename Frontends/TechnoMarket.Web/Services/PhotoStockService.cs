using TechnoMarket.Web.Models.PhotoStock;
using FreeCourse.Web.Services.Interfaces;
using TechnoMarket.Shared.Dtos;

namespace FreeCourse.Web.Services
{
    public class PhotoStockService : IPhotoStockService
    {
        private readonly HttpClient _httpClient;
        public PhotoStockService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> DeletePhoto(string photoUrl)
        {
            //Path içinde nokta olduğu için bu şekilde gösterdik.
            var response = await _httpClient.DeleteAsync($"photos?photoUrl={photoUrl}");
            return response.IsSuccessStatusCode;
        }

        public async Task<PhotoStockVM> UploadPhoto(IFormFile photo)
        {
            if (photo == null || photo.Length <= 0)
            {
                return null;
            }
            // => 55a12bfa-7138-4c0e-8a9b-03d484e17661.jpg
            var randomFilename = $"{Guid.NewGuid().ToString()}{Path.GetExtension(photo.FileName)}";

            using var ms = new MemoryStream();
            await photo.CopyToAsync(ms);

            var multipartContent = new MultipartFormDataContent();
            //photo ismini PhotoStock.Api beklediği için bu ismi verdik. Save metotunda.
            multipartContent.Add(new ByteArrayContent(ms.ToArray()), "photo", randomFilename);

            //Base Url startup tarafından gelecek
            var response = await _httpClient.PostAsync("photos", multipartContent);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var responseSuccess = await response.Content.ReadFromJsonAsync<CustomResponseDto<PhotoStockVM>>();

            return responseSuccess.Data;
        }
    }
}
