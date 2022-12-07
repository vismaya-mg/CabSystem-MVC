using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CabSystem.Areas.User.Controllers
{
    [Area("Users")]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> userManager;

        public UserController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            return View(db.Cabs.ToList());
        }
        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            var signeduser = await userManager.GetUserAsync(User);
            var user = await userManager.FindByEmailAsync(signeduser.Email);
            return View(new EditViewModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
            });
        }
        [HttpPost]
        public async Task<IActionResult> EditProfile(EditViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var user = await userManager.GetUserAsync(User);
            //var user = await userManager.FindByEmailAsync(signeduser.Email);
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            await userManager.UpdateAsync(user);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Booking()
        {
            return View();
        }
        public async Task<IActionResult> LocationSearch()
        {
            var user = await userManager.GetUserAsync(User);
            
            return View(new EditViewModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
            });
        }
}
