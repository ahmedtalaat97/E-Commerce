using E_Commerce_Core.DataTransferObjects;
using E_Commerce_Core.Enities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AdminDashboard.Controllers
{
    public class AdminController : Controller
    {

        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (ModelState.IsValid)
            {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
                if (user != null)
                {
                    var result= await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password,false);

                    //if (!result.Succeeded || ! await _userManager.IsInRoleAsync(user,"admin"))
                    //return View(loginDto);

                    await _signInManager.SignInAsync(user, false);
                    
                    return RedirectToAction("Index", "Home");   

                }

            }

            return View(loginDto);
        }


        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }


    }
}
