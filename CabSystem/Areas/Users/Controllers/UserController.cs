using CabSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

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
        [HttpPost]
        public async Task<IActionResult> Booking(BookViewModel model)
        {
            var user = await userManager.GetUserAsync(User);
            var driverList = db.Locations
              .Include(m => m.User)
              .Where(m => m.From == model.Pickup);

            db.Books.Add(new Book()
            {
                From = model.Pickup,
                To = model.Drop,
                Date = model.DateTime,
                UserId = user.Id,
                DriverName = "Anjana",
                Status = "Pending",
                Price=model.Cost,
                Payment="Unsuccesfull",
            });
            await db.SaveChangesAsync();
            return RedirectToAction("ListDrivers","User",new {Area = "Users", Id = model.Pickup,Date=model.DateTime });
        }

        [HttpGet]
        public async Task<IActionResult> ListDrivers(string Id)
        {
            Console.WriteLine(Id);
            var driverList = db.Locations
                .Include(m => m.User)
                .ThenInclude(m=>m.Cab)
                .Where(m => m.From == Id).ToList();
            
            return View(driverList);
        }


        //public async Task<IActionResult> ListDrivers(int id)
        //{
        //    var driverList = await db.Cabs
        //        .Include(m => m.User)
        //        .FirstOrDefaultAsync();

        //    var model = new BookConfirmViewModel()
        //    {
        //        Cab = driverList,
        //        User = driverList.User
        //    };
        //    return View(model);
        //}
        [HttpGet]

        public async Task<IActionResult> ListDriver(int id)
        {
            var user = await userManager.GetUserAsync(User);
            var loc = db.Locations.Where(i => i.Id == id).ToList();
            var book = db.Books.Where(i => i.UserId == user.Id).ToList();
            var driver = "";
            foreach (var item in loc)
            {
                driver = item.UserId;
            }
            var driverdetails = db.ApplicationUsers.Where(i => i.Id == driver).ToList();
            var drivername = "";
            foreach(var item in driverdetails)
            {
                drivername = item.FirstName;
            }
            foreach(var item in book)
            {
                item.DriverName = drivername;
            }
            
            await db.SaveChangesAsync();
            return RedirectToAction("BookConfirm","User", new { Area = "Users" });
        }
        public IActionResult BookConfirm()
        {

            return View();
        }
        public async Task<IActionResult> UpdateRequest(int id)
        {
            var bk = await db.Books.FindAsync(id);
            Console.WriteLine("Your id" + id);
            if (bk == null)
            {
                return NotFound();
            }

            bk.Status = "Approved";
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(ViewRequest));
        }
        public async Task<IActionResult> Payment(int id)
        {
            var bk = await db.Books.FindAsync(id);
            
            if (bk == null)
            {
                return NotFound();
            }

            bk.Payment = "Succesfull";
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(ViewRequest));
        }
        
        [HttpGet]
        public async Task<IActionResult> ViewRequest()
        {
            var user = await userManager.GetUserAsync(User);
            var locDetails = await db.Books.Where(m => m.UserId == user.Id).ToListAsync();
            return View(locDetails);

        }
    }
}