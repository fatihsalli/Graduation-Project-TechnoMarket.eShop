using Azure;
using TechnoMarket.AuthServer.Dtos;
using TechnoMarket.Shared.Dtos;

namespace TechnoMarket.AuthServer.Services.Interfaces
{
    //Repository oluşturmadık çünkü Identity kütüphanesi ile beraber hazır metotlar geldiği için repository katmanına gerek yoktur. 3 tane önemli class gelir bu kütüphaneyle birlikte 1-Usermanager (Kullanıcı hakkındaki işlemler için) 2-Rolemanager (Kullanıcı rolleri üzerinde değişiklikler için) 3-SignInManager (Login-logout işlemleri için)
    public interface IUserService
    {
        Task<CustomResponseDto<UserAppDto>> CreateUserAsync(CreateUserDto createUserDto);
        Task<CustomResponseDto<UserAppDto>> GetUserByNameAsync(string userName);
        //Kullanıcıya rol tanımlamak için aşağıdaki metotu yazdık.
        Task<CustomResponseDto<NoContentDto>> CreateUserRolesAsync(string userName);
    }
}
