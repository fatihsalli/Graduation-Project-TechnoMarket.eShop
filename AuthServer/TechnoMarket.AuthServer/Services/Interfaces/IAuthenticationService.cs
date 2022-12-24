using Azure;
using TechnoMarket.AuthServer.Dtos;
using TechnoMarket.Shared.Dtos;

namespace TechnoMarket.AuthServer.Services.Interfaces
{
    public interface IAuthenticationService
    {
        //Asenkron metot threadleri daha efektif kullanmamızı sağlayan metottur.
        Task<CustomResponseDto<TokenDto>> CreateTokenAsync(LoginDto loginDto);
        Task<CustomResponseDto<TokenDto>> CreateTokenByRefreshToken(string refreshToken);
        //Kullanıcı log-out yaptığında Refresh tokenı sonlandırma durumu için aşağıdaki metot tanımlandı. Ya da Refreshtoken çalındığı durumda bu metot ile server tarafında refresh tokenı null'a set edebiliriz.
        Task<CustomResponseDto<NoContentDto>> RevokeRefreshTokenAsync(string refreshToken);
        //Client ile birlikte üyelik durumu olmadan bir token alabiliriz. Aşaığıdaki metot bu durum için tanımlanmıştır. İçerisinde bir RefreshToken yok çünkü ClientId ve ClientSecret ile ben istediğim zaman Access Token alabilirim. Biz Client tarafında Client Id ve Secret bilgilerini dizin şeklinde AuthServer.Api-app.settings içinde tutacağız ancak 5 ten fazla olması durumunda serverda tutmak gerekir.
        CustomResponseDto<ClientTokenDto> CreateTokenByClient(ClientLoginDto clientLoginDto);

    }
}
