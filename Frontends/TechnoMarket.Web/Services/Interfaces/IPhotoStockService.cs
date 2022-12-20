using FreeCourse.Web.Models.PhotoStock;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace FreeCourse.Web.Services.Interfaces
{
    //PhotoStock.Api ile iletişimi servis üzerinden sağlayacağız.
    public interface IPhotoStockService
    {
        Task<PhotoStockVM> UploadPhoto(IFormFile photo);
        Task<bool> DeletePhoto(string photoUrl);


    }
}
