using DrawLots.Data;
using DrawLots.Models;
using DrawLots.Service;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DrawLots.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }


        public IActionResult Test(int id)
        {
            for (int i = 1; i < 60001; i++)
            {
                _db.Users.Add(new User { ActivityId = id, Name = $"User_{i:0000}" });
            }

            _db.SaveChanges();

            return View();
        }


        public IActionResult Index()
        {
            var activities = _db.Activities.ToList();

            return View(activities);
        }

        public IActionResult OnePage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult OnePage(Activities activities)
        {
            try
            {
                _db.Activities.Add(activities);

                _db.SaveChanges();

                return RedirectToAction("Index","User", new { id = activities.Id });
            }
            catch (Exception ex)
            {
                TempData["Message"] = "儲存錯誤，請洽開發者";

                return View();
            }
        }

        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}