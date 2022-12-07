using CabSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
namespace CabSystem.Areas.Admins.Controllers
{
    [Area("Admins")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> userManager;

        public AdminController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ViewCustomer()
        {
            return View(db.ApplicationUsers.ToList());

        }
        public IActionResult ViewDriver()
        {
            //if(User.IsInRole("CabDriver"))
            return View(db.ApplicationUsers.ToList());
            //var user = await userManager.GetUserAsync(User);
            //var Driver = await db.Where(m => m.us == user.Id).ToListAsync();
            //return View(Driver);
        }
        public async Task<IActionResult> Edit(string id)
        {
            var user = await db.ApplicationUsers.FindAsync(id);
            if (user == null)
                return NotFound();

            return View(new ApplicationUser()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber= user.PhoneNumber,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, ApplicationUser model)
        {
            var user = await db.ApplicationUsers.FindAsync(id);
            if (user == null)
                return NotFound();

            if (!ModelState.IsValid)
                return View(model);

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string id)
        {
            var user = await db.ApplicationUsers.FindAsync(id);
            if (user == null)
                return NotFound();

            db.ApplicationUsers.Remove(user);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
