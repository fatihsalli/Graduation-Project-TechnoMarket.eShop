using Microsoft.Extensions.Options;
using TechnoMarket.Web.Models;

namespace FreeCourse.Web.Helpers
{
    //Biz kursları aldığımızda Url olarak fotoğrafın ismi geliyor. Biz bu classta bunu PhotoStockdan alacak şekilde çeviriyoruz.
    public class PhotoHelper
    {
        private readonly ServiceApiSettings _serviceApiSettings;

        public PhotoHelper(IOptions<ServiceApiSettings> serviceApiSettings)
        {
            _serviceApiSettings = serviceApiSettings.Value;
        }

        public string GetPhotoStockUrl(string photoUrl)
        {
            return $"{_serviceApiSettings.PhotoStockUri}/photos/{photoUrl}";
        }

    }
}
