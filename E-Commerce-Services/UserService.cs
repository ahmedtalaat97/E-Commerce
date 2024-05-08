using E_Commerce_Core.DataTransferObjects;
using E_Commerce_Core.Enities.Identity;
using E_Commerce_Core.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Services
{
    public class UserService : IUserService
        
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly SignInManager<ApplicationUser> _signInManager;
        
        private readonly ITokenService _tokenService;

        public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public async Task<UserDto?> LoginAsync(LoginDto dto)
        {
            //Email=> User=>password=>token

            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user is not null)
            {
                var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password ,false);
                if (result.Succeeded)
                    return new UserDto
                    {
                        DisplayName=user.DisplayName,
                        Email=user.Email,
                        Token=_tokenService.GenerateToken(user)


                    };
            }

            return null;

     
        }

        public async Task<UserDto> RegisterAsync(RegisterDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user is not null) throw new Exception("Email Exists");

            var appUser = new ApplicationUser
            {
                DisplayName = dto.DisplayName,
                Email = dto.Email,
                UserName = dto.DisplayName
            };

            var result = await _userManager.CreateAsync(appUser, dto.Password);
            if (!result.Succeeded) throw new Exception($"Error{result.Errors}");

            return new UserDto {DisplayName=appUser.DisplayName,
                Email=appUser.Email,
                Token=_tokenService.GenerateToken(appUser)
            
            };


            
        }
    }
}
