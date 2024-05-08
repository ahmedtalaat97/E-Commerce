using AdminDashboard.Models;
using E_Commerce_Core.Enities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminDashboard.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {

            var user = await _userManager.Users.ToListAsync();

            var mappedUser = user.Select(u => new UserViewModel {
            Id = u.Id,
            DisplayName = u.DisplayName,
            UserName = u.UserName,
            PhoneNumber = u.PhoneNumber,
            Email = u.Email,
            Roles=_userManager.GetRolesAsync(u).Result
            
            }).ToList();


            return View(mappedUser);
        }


        [HttpGet]
        public async Task<IActionResult> Edit (string id)
        {
            var user=await _userManager.FindByIdAsync(id);

            var allRoles=await _roleManager.Roles.ToListAsync();

            var userRoleVM= new UserRoleViewModel ()
            
            { Id = user.Id,
                UserName=user.UserName ,
                Roles=allRoles.Select(r=>new RoleViewModel 
                {
                    Id=r.Id,
                    Name=r.Name,
                    IsSelected= _userManager.IsInRoleAsync(user,r.Name).Result
                    
                
                
                }).ToList()
            };

            return View(userRoleVM);

        }


        [HttpPost]

        public async Task<IActionResult> Edit(UserRoleViewModel userRoleVM)
        {
            var user= await _userManager.FindByIdAsync (userRoleVM.Id);

            var userRoles=await _userManager.GetRolesAsync(user);

            foreach (var role in userRoleVM.Roles) {
            
                if(userRoles.Contains(role.Name)&& !role.IsSelected)
                {
                    await _userManager.RemoveFromRoleAsync(user,role.Name);
                }


                if (!userRoles.Contains(role.Name) && role.IsSelected)
                {
                    await _userManager.AddToRoleAsync(user, role.Name);
                }


               

            }

            return RedirectToAction(nameof(Index));

        }

    }
}
