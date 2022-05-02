using DrawLots.Data;
using DrawLots.Models;
using DrawLots.Service;
using Microsoft.AspNetCore.Mvc;

namespace DrawLots.Controllers
{
    public class DrawLotsController : Controller
    {
        private readonly ApplicationDbContext _db;

        private readonly LotService _lotService;

        public DrawLotsController(ApplicationDbContext db, LotService lotService)
        {
            _db = db;
            _lotService = lotService;
        }

        public IActionResult Check(int id)
        {
            var users = _db.Users.Where(p => p.ActivityId == id);
            var activity = _db.Activities.Find(id);

            ViewBag.id = id;
            ViewBag.ActivityName = activity?.Name;

            return View(users.ToList());
        }

        public IActionResult History(int id)
        {
            var lot = _db.Histories.Where(p => p.ActivityId == id);

            return View(lot.ToList());
        }

        public IActionResult Again(int id)
        {
            var histories = _db.Histories.Where(p => p.ActivityId == id);

            _db.Histories.RemoveRange(histories);

            _db.SaveChanges();

            return RedirectToAction("Index","User", new { id });
        }

        public IActionResult Execute(int id, int? recordId = null, int? lotValue = null)
        {
            ViewBag.id = id;
            ViewBag.recordId = recordId;

            if (lotValue != null)
            {
                var ran = new Random();

                var users = GetUser(recordId.Value);

                var lot = _db.Lots.FirstOrDefault(p => p.Id == lotValue);

                var 人數 = lot.人數;

                var shuffle = users.Shuffle().ToList();

                var result = shuffle.Take(人數.Value);

                Save(id, lot.獎項, result);
                UpdateRecord(recordId.Value, 人數.Value, shuffle);

                ViewBag.獎項 = lot.獎項;


                return View(result.ToList());
            }
            else
            {
                var users = _db.Users.Where(p => p.ActivityId == id).Select(p => p.Name);
                var record = new LotRecord { Name = string.Join("|", users) };

                _db.LotRecord.Add(record);
                _db.SaveChanges();

                ViewBag.recordId = record.Id;
            }

            return View();
        }

        public void UpdateRecord(int recordId ,int people, IEnumerable<string> shuffle) 
        {
            var 剩下的 = shuffle.Skip(people);

            var names = string.Join("|", 剩下的);

            var record =  _db.LotRecord.Find(recordId);

            record.Name = names;

            _db.SaveChanges();
        }

        public List<string> GetUser(int recordId)
        {
            var record = _db.LotRecord.Find(recordId);

            var names = record.Name.Split("|");

            return names.ToList();
        }

        public void Save(int id, string name, IEnumerable<string> users)
        {
            if (User != null)
            {
                var history = string.Join("|", users);

                _db.Histories.Add(new History { ActivityId = id, 獎項 = name, Name = history });

                _db.SaveChanges();
            }
        }

        public IActionResult LoadTable(int id)
        {
            try
            {
                var users = _db.Users.Where(p => p.ActivityId == id);

                ViewBag.Id = id;

                return PartialView("_UserTable", users.ToList());
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}
