﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TechnoMarket.Services.Customer.Dtos;
using TechnoMarket.Services.Customer.Models;
using TechnoMarket.Services.Customer.Services.Interfaces;
using TechnoMarket.Shared.ControllerBases;
using TechnoMarket.Shared.Dtos;

namespace TechnoMarket.Services.Customer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : CustomBaseController
    {
        private readonly ICustomerService _customerService;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public UsersController(ICustomerService customerService, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _customerService = customerService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> RegisterUser(RegisterDto registerDto)
        {
            AppUser newUser = new AppUser()
            {
                UserName = registerDto.Username,
                Email = registerDto.Email,
                CustomerId=new Guid(registerDto.CustomerId)
            };

            var result=await _userManager.CreateAsync(newUser, registerDto.Password);

            if (!result.Succeeded)
            {
                //Hata
            }

            return CreateActionResult(CustomResponseDto<bool>.Success(204,result.Succeeded));
        }

        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.Username);

            if (user == null)
            {
                //Hata
            }

            var result=await _signInManager.PasswordSignInAsync(user, loginDto.Password,false,false);

            return CreateActionResult(CustomResponseDto<bool>.Success(200, result.Succeeded));
        }
    }
}
