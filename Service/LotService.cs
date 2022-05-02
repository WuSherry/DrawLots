using DrawLots.Data;
using DrawLots.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DrawLots.Service
{
    public class LotService
    {
        private readonly ApplicationDbContext _db;

        public LotService(ApplicationDbContext db) 
        {
            _db = db;
        }

        public List<SelectListItem> GetSelectList(int id) 
        {
            var history = _db.Histories.Where(p => p.ActivityId == id).Select(p => p.獎項).Distinct().ToList();

            var lots = _db.Lots.Where(p=>p.ActivityId == id && !history.Contains(p.獎項));

            var select = lots.Select(p => new SelectListItem
            {
                Text = p.獎項,
                Value = p.Id.ToString()
            });

            return select.ToList();
        }

        public bool CheckCanLot(int id) 
        {
            var userCount = _db.Users
                .Count(p => p.ActivityId == id);

            var itemCount = _db.Lots
                .Where(p => p.ActivityId == id).Sum(p => p.人數);

            if (itemCount.HasValue)
            {
                if (itemCount <= userCount)
                {
                    return true;
                }
            }

            return false;
        }

        public List<Lots> GetById(int id) 
        {
            var lots = _db.Lots.Where(p => p.ActivityId == id).ToList();

            return lots;
        }
    }
}
