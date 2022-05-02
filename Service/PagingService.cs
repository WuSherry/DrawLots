using ReflectionIT.Mvc.Paging;

namespace DrawLots.Service
{
    public class PagingService
    {
        public static PagingList<T> ToList<T, TCondition>(ISearchService<T, TCondition> searchService)
    where T : class
    where TCondition : new()
        {
            var pageResult = PagingList.Create(
                searchService.Results, 50, searchService.PageIndex ?? 1);

            pageResult.RouteValue = PagingRouteValueService.GetValue(searchService.Condition);

            return pageResult;
        }
    }
}
