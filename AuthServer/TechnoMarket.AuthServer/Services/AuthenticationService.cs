using Azure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Runtime.CompilerServices;
using TechnoMarket.AuthServer.Data;
using TechnoMarket.AuthServer.Dtos;
using TechnoMarket.AuthServer.Models;
using TechnoMarket.AuthServer.Services.Interfaces;
using TechnoMarket.AuthServer.Settings;
using TechnoMarket.Shared.Dtos;

namespace TechnoMarket.AuthServer.Services
{

    public class AuthenticationService
    {
        private readonly List<Client> _clients;
        private readonly ITokenService _tokenservice;
        private readonly UserManager<UserApp> _userManager;
        private readonly AppDbContext _context;
        private readonly DbSet<UserRefreshToken> _dbSet;

        public AuthenticationService(IOptions<List<Client>> optionClients, ITokenService tokenservice, UserManager<UserApp> userManager,AppDbContext context)
        {
            _clients = optionClients.Value;
            _tokenservice = tokenservice;
            _userManager = userManager;
            _context = context;
            _dbSet=context.Set<UserRefreshToken>();
        }

        //Üyelik gerektiren api için kullanılacak.
        public async Task<CustomResponseDto<TokenDto>> CreateTokenAsync(LoginDto loginDto)
        {
            if (loginDto == null)
            {
                throw new ArgumentNullException(nameof(loginDto));
            }

            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null)
            {
                //TokenDto döndük ama boş olarak.Response model tokenDto istediği için Fail ile TokenDto null olarak gönderiyoruz. Kötü niyetli kullanıcılar hangisinin yanlış olduğunu anlayamasın diye Email veya şifre dedik.
                return CustomResponseDto<TokenDto>.Fail(400, "Email or Password is wrong");
            }

            if (!await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                return CustomResponseDto<TokenDto>.Fail(400, "Email or Password is wrong");
            }

            var token = _tokenservice.CreateToken(user);

            //Sistemde refresh token olup olmadığını kontrol ediyoruz.
            var userRefreshToken = await _dbSet.Where(x => x.UserId == user.Id).SingleOrDefaultAsync();
            //Sistemde refresh token olmadığı için yeniden ürettik.
            if (userRefreshToken == null)
            {
                await _dbSet.AddAsync(new UserRefreshToken { UserId = user.Id, Code = token.RefreshToken, Expiration = token.ResfreshTokenExpiration });
            }
            else
            {
                userRefreshToken.Code = token.RefreshToken;
                userRefreshToken.Expiration = token.ResfreshTokenExpiration;
            }

            await _context.SaveChangesAsync();
            return CustomResponseDto<TokenDto>.Success(200,token);
        }

        //Client yani üyelik gerektirmen api için kullanılacak.
        public CustomResponseDto<ClientTokenDto> CreateTokenByClient(ClientLoginDto clientLoginDto)
        {
            //Appsettings üzerinden oluşturduğumuz client dizininde bize gelen id var mı yok mu buna bakıyoruz.
            var client = _clients.SingleOrDefault(x => x.ClientId == clientLoginDto.ClientId && x.ClientSecret == clientLoginDto.ClientSecret);

            if (client == null)
            {
                return CustomResponseDto<ClientTokenDto>.Fail(404,"ClientId or ClientSecret not found");
            }

            var token = _tokenservice.CreateTokenByClient(client);
            return CustomResponseDto<ClientTokenDto>.Success(200,token);
        }

        public async Task<CustomResponseDto<TokenDto>> CreateTokenByRefreshToken(string refreshToken)
        {
            //Sistemde refresh token olup olmadığını kontrol ediyoruz.
            var existRefreshToken = await _dbSet.Where(x => x.Code == refreshToken).SingleOrDefaultAsync();

            if (existRefreshToken == null)
            {
                return CustomResponseDto<TokenDto>.Fail(404,"Refresh token not found");
            }
            //Userı sistemden yakaladık.
            var user = await _userManager.FindByIdAsync(existRefreshToken.UserId);

            if (user == null)
            {
                return CustomResponseDto<TokenDto>.Fail(404,"User Id not found");
            }

            var token = _tokenservice.CreateToken(user);
            existRefreshToken.Code = token.RefreshToken;
            existRefreshToken.Expiration = token.ResfreshTokenExpiration;
            await _context.SaveChangesAsync();
            return CustomResponseDto<TokenDto>.Success(200,token);
        }

        public async Task<CustomResponseDto<NoContentDto>> RevokeRefreshTokenAsync(string refreshToken)
        {
            var existRefreshToken = await _dbSet.Where(x => x.Code == refreshToken).SingleOrDefaultAsync();

            if (existRefreshToken == null)
            {
                return CustomResponseDto<NoContentDto>.Fail(404,"Refresh token not found");
            }

            _dbSet.Remove(existRefreshToken);
            await _context.SaveChangesAsync();
            return CustomResponseDto<NoContentDto>.Success(200);
        }
    }

}
