using System.Text.Json;
using TechnoMarket.Shared.Dtos;

namespace TechnoMarket.Shopping.Aggregator.Extensions
{
    public static class HttpClientExtensions
    {
        public static async Task<T> ReadContentAs<T>(this HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
                throw new ApplicationException($"Something went wrong calling the API: {response.ReasonPhrase}");

            var responseSuccess = await response.Content.ReadFromJsonAsync<CustomResponseDto<T>>();
            return responseSuccess.Data;
        }
    }
}
