using CabSystem.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CabSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(
            ApplicationDbContext db,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid details");
                return View(model);
            }

            var res = await _signInManager.PasswordSignInAsync(user, model.Password, false, true);

            if (res.Succeeded)
            {
                //return RedirectToAction("Index", "Home", new {Area=""});
                //return Redirect("/");
                {
                    if (await _userManager.IsInRoleAsync(user, "Admin"))
                    {
                        return RedirectToAction("Index", "Admin", new { Area = "Admins" });
                    }
                    else if(await _userManager.IsInRoleAsync(user, "User"))
                    {
                        return RedirectToAction("Index", "User", new { Area = "Users" });
                    }
                    else
                    {
                        return RedirectToAction("Index", "Driver", new { Area = "Drivers" });

                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                UserName = Guid.NewGuid().ToString().Replace("-", "")
            };

            var role = Convert.ToString(model.UserTypes);
            var res = await _userManager.CreateAsync(user, model.Password);
            await _userManager.AddToRoleAsync(user, role);

            if (res.Succeeded)
            {
                return RedirectToAction(nameof(Login));
            }

            ModelState.AddModelError("", "An Error Occoured");
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> GenerateData()
        {
            await _roleManager.CreateAsync(new IdentityRole() { Name = "Admin" });
            await _roleManager.CreateAsync(new IdentityRole() { Name = "User" });
            await _roleManager.CreateAsync(new IdentityRole() { Name = "CabDriver" });

            var users = await _userManager.GetUsersInRoleAsync("Admin");
            if (users.Count == 0)
            {
                var appUser = new ApplicationUser()
                {
                    FirstName = "Admin",
                    LastName = "User",
                    Email = "admin@admin.com",
                    UserName = "admin",
                };
                var res = await _userManager.CreateAsync(appUser, "Pass@123");
                await _userManager.AddToRoleAsync(appUser, "Admin");
            }
            return Ok("Data generated");
        }
    }
}
