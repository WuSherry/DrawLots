using DrawLots.Models;
using Microsoft.Extensions.Caching.Memory;

namespace DrawLots.Service
{
    public abstract class ISearchService<T, TCondition> where TCondition : new()
    {
        public IEnumerable<T> Results { get; set; }

        public int? PageIndex
        {
            get
            {
                return _cache.Get<int?>(_pageKey);
            }
            set
            {
                _cache.Set(_pageKey, value);
            }
        }

        public TCondition Condition
        {
            get
            {
                var conditions = _cache.Get<TCondition>(_conditinoKey);

                return conditions ?? new TCondition();
            }
            set
            {
                _cache.Set(_conditinoKey, value);
            }
        }

        protected readonly IMemoryCache _cache;
        protected readonly IEnumerable<T> _baseData;
        protected readonly int? _pageIndex;
        protected readonly TCondition _condition;

        private string _pageKey, _conditinoKey;

        protected ISearchService(
            IMemoryCache cache,
            IEnumerable<T> data,
            TCondition condition,
            int? pageIndex = null)
        {
            _cache = cache;
            _baseData = data;
            _condition = condition;
            _pageIndex = pageIndex;

            SetKeyAndPageIndex();
        }

        protected ISearchService(IMemoryCache cache)
        {
            _cache = cache;

            SetKeyAndPageIndex();
        }


        private void SetKeyAndPageIndex()
        {
            SetKeys();

            if (_pageIndex != null)
            {
                PageIndex = _pageIndex;
            }
        }
        private void SetKeys()
        {
            var typeName = typeof(T).Name;
            _pageKey = typeName + nameof(PageIndex);
            _conditinoKey = typeName + nameof(Condition);
        }

        public IEnumerable<T> Search()
        {
            if ((_condition as SearchCondition).IsFromSearch)
            {
                Condition = _condition;
            }

            Results = SearchImp();

            return Results;
        }

        protected abstract IEnumerable<T> SearchImp();

        public void Clear()
        {
            Condition = new TCondition();

            PageIndex = 1;
        }
    }
}
