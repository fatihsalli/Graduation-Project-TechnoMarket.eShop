using Microsoft.AspNetCore.Mvc;
using TechnoMarket.AuthServer.Dtos;
using TechnoMarket.AuthServer.Services.Interfaces;
using TechnoMarket.Shared.ControllerBases;

namespace TechnoMarket.AuthServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : CustomBaseController
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        //api/auth/createtoken
        [HttpPost]
        public async Task<IActionResult> CreateToken(LoginDto loginDto)
        {
            var result = await _authenticationService.CreateTokenAsync(loginDto);
            //result generic aldığı için içerisinden generic ne olduğunu çıkartıyor.
            return CreateActionResult(result);
        }

        [HttpPost]
        public IActionResult CreateTokenByClient(ClientLoginDto clientLoginDto)
        {
            var result = _authenticationService.CreateTokenByClient(clientLoginDto);
            return CreateActionResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> RevokeRefreshToken(RefreshTokenDto refteshTokenDto)
        {
            var result = await _authenticationService.RevokeRefreshTokenAsync(refteshTokenDto.RefreshTokenCode);
            return CreateActionResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTokenByRefreshToken(RefreshTokenDto refteshTokenDto)
        {
            var result = await _authenticationService.CreateTokenByRefreshToken(refteshTokenDto.RefreshTokenCode);
            return CreateActionResult(result);
        }



    }
}
