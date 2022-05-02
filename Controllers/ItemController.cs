using DrawLots.Data;
using DrawLots.Models;
using DrawLots.Service;
using Microsoft.AspNetCore.Mvc;

namespace DrawLots.Controllers
{
    public class ItemController : Controller
    {
        private readonly ApplicationDbContext _db;

        private readonly LotService _lotService;

        public ItemController(ApplicationDbContext db, LotService lotService)
        {
            _db = db;
            _lotService = lotService;
        }

        public IActionResult Index(int id)
        {
            return View(id);
        }

        public IActionResult LoadTable(int id)
        {
            try
            {
                var lots = _db.Lots.Where(p => p.ActivityId == id);

                return PartialView("_Table", lots.ToList());
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(Lots lots)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (string.IsNullOrEmpty(lots.獎項) || lots.人數 == null) 
                    {
                        return Json(JsonResultService.Get(false, $"新增失敗，欄位不能為空"));
                    }

                    var isExist = _db.Lots.Any(p => p.ActivityId == lots.ActivityId && p.獎項 == lots.獎項);

                    if (isExist) return Json(JsonResultService.Get(false, $"{lots.獎項} 新增失敗，獎項重複"));

                    _db.Lots.Add(lots);
                    await _db.SaveChangesAsync();

                    return Json(JsonResultService.Get(true, $"新增成功"));
                }
                else
                {
                    return Json(JsonResultService.Get(false, $"新增失敗，請檢查欄位輸入正確"));
                }
            }
            catch (Exception ex)
            {
                return Json(JsonResultService.Get(false, $"新增失敗，請重新整理"));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var lot = _db.Lots.Find(id);

                if (lot != null)
                {
                    _db.Lots.Remove(lot);
                    await _db.SaveChangesAsync();
                }

                return Json(JsonResultService.Success);
            }
            catch (Exception ex)
            {
                return Json(JsonResultService.Fail);
            }
        }

        public IActionResult NextStep(int id)
        {
            var canLot = _lotService.CheckCanLot(id);

            if (canLot)
            {
                return RedirectToAction("Check", "DrawLots", new { id });
            }
            else 
            {
                TempData["Message"] = "處理失敗，請確認參加人數大於獎項人數";

                return RedirectToAction("Index", new { id });
            }
        }
    }
}
