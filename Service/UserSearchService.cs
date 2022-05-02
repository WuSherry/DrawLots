using DrawLots.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace DrawLots.Service
{
    public class UserSearchService<T, TCondition> : ISearchService<T, TCondition> where TCondition : new()
    {
        public UserSearchService(
            IMemoryCache cache,
            IQueryable<T> data,
            TCondition condition,
            int? pageIndex = null) : base(cache, data, condition, pageIndex) { }

        public UserSearchService(IMemoryCache cache) : base(cache) { }

        protected override IEnumerable<T> SearchImp()
        {
            var condition = Condition as User;
            var data = _baseData as IQueryable<User>;

            if (condition?.Name != null)
            {
                data = data.Where(p =>
                        EF.Functions.Like(p.Name, $"%{condition.Name}%")
                   );
            }

            return (data as IEnumerable<T>).Reverse();
        }
    }
}
