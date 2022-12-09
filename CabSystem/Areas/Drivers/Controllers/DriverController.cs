namespace CabSystem.Areas.Drivers.Controllers
{
    [Area("Drivers")]

    public class DriverController : Controller
    {

        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> userManager;

        public DriverController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(User);
            var locDetails = await db.Cabs.Where(m => m.UserId == user.Id).ToListAsync();
            return View(locDetails);

        }
        [HttpGet]
        public IActionResult AddCab()
        {
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> AddCab(CabViewModel cab)
        {
            var user = await userManager.GetUserAsync(User);
            if (!ModelState.IsValid)
            {
                return View(cab);
            }
            db.Cabs.Add(new Cab()
            {
                Vehicle = cab.Vehicle,
                VechicleNumber = cab.VechicleNumber,
                Model = cab.Model,
                Description = cab.Description,
                UserId = user.Id,
            });
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Driver", new { Area = "Drivers" });
        }
        [HttpGet]
        public async Task<IActionResult> EditCab(int id)
        {
            var user = await db.Cabs.FindAsync(id);
            if (user == null)
                return NotFound();

            return View(new CabViewModel()
            {
                VechicleNumber = user.VechicleNumber,
                Vehicle = user.Vehicle,
                Model = user.Model,
                Description = user.Description,
            });

        }
        [HttpPost]
        public async Task<IActionResult> EditCab(int id, CabViewModel models)
        {
            var vehicles = await db.Cabs.FindAsync(id);
            if (vehicles == null)
                return NotFound();

            if (!ModelState.IsValid)
                return View(models);
            vehicles.VechicleNumber = models.VechicleNumber;
            vehicles.Vehicle = models.Vehicle;
            vehicles.Model = models.Model;
            vehicles.Description = models.Description;
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]

        public IActionResult AddLocation()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddLocation(LocationViewModel model)
        {
            var user = await userManager.GetUserAsync(User);
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            db.Locations.Add(new Location()
            {
                From = model.From,
                To = model.To,
                UserId = user.Id,
            });
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Driver", new { Area = "Drivers" });
        }

        public async Task<IActionResult> ViewLocation()
        {
            var user = await userManager.GetUserAsync(User);
            var locDetails = await db.Locations.Where(m => m.UserId == user.Id).ToListAsync();
            return View(locDetails);

        }

        public async Task<IActionResult> Delete(int id)
        {
            var vehicles = await db.Cabs.FindAsync(id);
            if (vehicles == null)
                return NotFound();

            db.Cabs.Remove(vehicles);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> DeleteLocation(int id)
        {
            var Loc = await db.Locations.FindAsync(id);
            if (Loc == null)
                return NotFound();

            db.Locations.Remove(Loc);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]

        public async Task<IActionResult> ViewRequest()
        {
            var user = await userManager.GetUserAsync(User);
            var locDetails = await db.Books.Where(m => m.DriverName == user.FirstName).ToListAsync();
            return View(locDetails);

        }

        [HttpGet]
        public async Task<IActionResult> UpdateRequest(int id)
        {
            var bk = await db.Books.FindAsync(id);
            Console.WriteLine("Your id" +id);
            if (bk == null)
            {
                return NotFound();
            }

            bk.Status = "Approved";
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(ViewRequest));
        }
    }
}
