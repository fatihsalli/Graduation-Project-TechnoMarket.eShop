using FreeCourse.Services.PhotoStock.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TechnoMarket.Shared.ControllerBases;
using TechnoMarket.Shared.Dtos;
using TechnoMarket.Shared.Exceptions;

namespace FreeCourse.Services.PhotoStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : CustomBaseController
    {
        private readonly ILogger<PhotosController> _logger;
        public PhotosController(ILogger<PhotosController> logger)
        {
            _logger = logger;
        }

        #region Neden CancellationToken Aldık?
        //Cancellation tokenı neden aldık? Buraya bir fotoğraf geldiğinde örneğin 20 sn sürüyor diyelim isteği gönderen işlem tamamlanmadan iptal ederse işlem devam etmesin sonlansın diye. Asenkron başlayan bir işlemi hata fırlatarak sonlandırabilirsiniz. Cancellation Token da hata fırlatarak işlemi sonlandırır. 
        #endregion
        [HttpPost]
        [ProducesResponseType(typeof(CustomResponseDto<PhotoDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(CustomResponseDto<NoContentDto>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PhotoSave(IFormFile photo, CancellationToken cancellationToken)
        {
            if (photo != null && photo.Length > 0)
            {
                //Path ini oluşturuyoruz.
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photo.FileName);

                using var stream = new FileStream(path, FileMode.Create);
                //Photo yu kaydederken tarayıcı kapanır veya işlem sonlanırsa diye cancellation tokenı da veriyoruz. (İşlemi sonlandırması için)
                await photo.CopyToAsync(stream, cancellationToken);
                //İsteği yapana pathi dönüyoruz.
                var returnPath = photo.FileName;
                //var photoDto= new PhotoDto { Url= returnPath };
                PhotoDto photoDto = new() { Url = returnPath };

                return CreateActionResult(CustomResponseDto<PhotoDto>.Success(200, photoDto));
            }

            _logger.LogError($"Photo is empty!");
            throw new ClientSideException($"Photo is empty!");
        }

        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(CustomResponseDto<NoContentDto>), (int)HttpStatusCode.NotFound)]
        public IActionResult PhotoDelete(string photoUrl)
        {
            //Buradaki ifade yan yana aşağıda verdiğimiz pathleri birleştirir.
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photoUrl);

            //Path var mı yok mu kontrol ediyoruz.
            if (!System.IO.File.Exists(path))
            {
                _logger.LogError($"Photo with url ({photoUrl}) didn't find in the database.");
                throw new NotFoundException($"Photo with url ({photoUrl}) didn't find in the database.");
            }
            //Var ise siliyoruz.
            System.IO.File.Delete(path);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
    }
}
