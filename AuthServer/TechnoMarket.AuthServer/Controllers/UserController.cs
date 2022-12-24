using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechnoMarket.AuthServer.Dtos;
using TechnoMarket.AuthServer.Services.Interfaces;
using TechnoMarket.Shared.ControllerBases;

namespace TechnoMarket.AuthServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : CustomBaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserDto createUserDto)
        {
            return CreateActionResult(await _userService.CreateUserAsync(createUserDto));
        }

        //Authorize ile işaretledik token istediğimiz için.
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            //Name'i nasıl buluyor sisteme kullanıcı giriş yaptığında "TokenService-GetClaims-ClaimTypes.Name,userApp.UserName" kısmından buluyor. İsimlendirmeyi doğru şekilde verdiğimizden context üzerinden kendi buluyor.
            return CreateActionResult(await _userService.GetUserByNameAsync(HttpContext.User.Identity.Name));
        }

        //Diğer post ile çakışmaması için action ismini de verdik.
        [HttpPost("CreateUserRoles/{userName}")]
        public async Task<IActionResult> CreateUserRoles(string userName)
        {
            return CreateActionResult(await _userService.CreateUserRolesAsync(userName));
        }




    }
}
