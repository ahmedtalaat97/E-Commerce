using E_Commerce.API.Errors;
using E_Commerce_Core.DataTransferObjects;
using E_Commerce_Core.Interfaces.Services;
using E_Commerce_Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountsController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost]

        public async Task<ActionResult<UserDto>> Login(LoginDto input)
        {
            var user= await _userService.LoginAsync(input);
            return  user is not null? Ok(user) : Unauthorized(new ApiResponse(401,"Incorrect Email or Password"));

        }


        [HttpPost]

        public async Task<ActionResult<UserDto>> Register(RegisterDto input)
        {
           
            return Ok(await _userService.RegisterAsync(input)) ;

        }
    }
}
