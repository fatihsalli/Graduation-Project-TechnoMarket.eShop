using TechnoMarket.AuthServer.Dtos;
using TechnoMarket.AuthServer.Models;
using TechnoMarket.AuthServer.Settings;

namespace TechnoMarket.AuthServer.Services.Interfaces
{
    public interface ITokenService
    {
        TokenDto CreateToken(UserApp appUser);
        ClientTokenDto CreateTokenByClient(Client client);
    }
}
