namespace DrawLots.Service
{
    public class PagingRouteValueService
    {
        public static RouteValueDictionary GetValue(object condition)
        {
            var properties = condition.GetType().GetProperties();
            var dic = new RouteValueDictionary();

            foreach (var property in properties)
            {
                dic.Add(property.Name, ObjectService.GetValue(condition, property.Name));
            }

            return dic;
        }
    }
}
