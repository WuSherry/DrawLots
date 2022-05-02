using DrawLots.Data;
using DrawLots.Models;
using DrawLots.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace DrawLots.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IMemoryCache _cache;


        public UserController(ApplicationDbContext db, IMemoryCache cache)
        {
            _db = db;
            _cache = cache;
        }

        public IActionResult Index(int id)
        {
            return View(id);
        }

        public IActionResult LoadTable(int id, User condition = null, int? pageIndex = 1)
        {
            try
            {
                condition.ActivityId = id;

                var users = _db.Users.Where(p => p.ActivityId == id);

                var searchService =
                    new UserSearchService<User, User>(_cache, users, condition, pageIndex);

                searchService.Search();

                var pageResult = PagingService.ToList(searchService);

                ViewBag.Condition = searchService.Condition;
                ViewBag.Id = id;
                ViewBag.page = pageResult.PageIndex;
                ViewBag.Count = users.Count();
                ViewBag.maxPage = pageResult.PageCount;


                return PartialView("_Table", pageResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        public IActionResult ClearSearch()
        {
            var searchService =
                 new UserSearchService<User, User>(_cache);

            searchService.Clear();

            return Json("");
        }

        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var isExist = _db.Users.Any(p => p.ActivityId == user.ActivityId && p.Name == user.Name);

                    if (isExist) return Json(JsonResultService.Get(false, $"{user.Name} 新增失敗，姓名重複"));

                    _db.Users.Add(user);
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
                var user = _db.Users.Find(id);

                if (user != null)
                {
                    _db.Users.Remove(user);
                    await _db.SaveChangesAsync();
                }

                return Json(JsonResultService.Success);
            }
            catch (Exception ex)
            {
                return Json(JsonResultService.Fail);
            }
        }
    }
}
