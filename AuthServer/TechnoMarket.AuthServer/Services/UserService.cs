using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TechnoMarket.AuthServer.Dtos;
using TechnoMarket.AuthServer.Models;
using TechnoMarket.AuthServer.Services.Interfaces;
using TechnoMarket.Shared.Dtos;

namespace TechnoMarket.AuthServer.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<UserApp> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<UserApp> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<CustomResponseDto<UserAppDto>> CreateUserAsync(CreateUserDto createUserDto)
        {
            var user = new UserApp
            {
                Email = createUserDto.Email,
                UserName = createUserDto.UserName
            };
            //Passwordu hashleyip kendisi dolduruyor.
            var result = await _userManager.CreateAsync(user, createUserDto.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description).ToList();
                return CustomResponseDto<UserAppDto>.Fail(400, errors);
            }

            return CustomResponseDto<UserAppDto>.Success(200, _mapper.Map<UserAppDto>(user));
        }
        //Kullanıcıya rol eklemek için bu methodu tanımladık.
        public async Task<CustomResponseDto<NoContentDto>> CreateUserRolesAsync(string userName)
        {
            if (!await _roleManager.RoleExistsAsync("admin"))
            {
                await _roleManager.CreateAsync(new() { Name = "admin" });
                await _roleManager.CreateAsync(new() { Name = "manager" });
            }

            var user = await _userManager.FindByNameAsync(userName);
            await _userManager.AddToRoleAsync(user, "admin");
            await _userManager.AddToRoleAsync(user, "manager");
            //Status code'u aşağıdaki gibi de yazabiliriz.
            return CustomResponseDto<NoContentDto>.Success(StatusCodes.Status201Created);
        }

        public async Task<CustomResponseDto<UserAppDto>> GetUserByNameAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                return CustomResponseDto<UserAppDto>.Fail(404, "UserName not found");
            }

            return CustomResponseDto<UserAppDto>.Success(200, _mapper.Map<UserAppDto>(user));
        }
    }
}
