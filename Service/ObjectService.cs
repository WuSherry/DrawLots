using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DrawLots.Service
{
    public class ObjectService
    {
        public static object GetValue(object obj, string propertyName)
        {

            if (obj == null) return null;

            try
            {
                return obj.GetType().GetProperty(propertyName)?.GetValue(obj, null);
            }

            catch (Exception ex)
            {
                throw;
            }

        }

        public static void SetValue(object obj, string propertyName, object value)
        {
            if (obj == null) return;

            try
            {
                var property = obj.GetType().GetProperty(propertyName);

                if (property != null)
                {
                    object safeValue;
                    Type t = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;

                    if (t.IsEnum)
                        safeValue = Enum.Parse(t, value.ToString());
                    else
                        safeValue = Convert.ChangeType(value, t);

                    property.SetValue(obj, safeValue);

                }

            }
            catch (Exception ex)
            {
            }
        }


        public static bool CheckValuesIsNullOrEmpty(object obj,
            IEnumerable<string> exceptionProperties = null,
            bool checkBool = false)
        {
            if (obj == null)
                return true;

            var result = GetProperties(obj).Where(p =>
            {
                if (exceptionProperties != null)
                    return !exceptionProperties.Contains(p.Name);

                return true;

            }).All(p =>
            {
                var value = p.GetValue(obj);

                var result = value == null || value?.ToString() == "";

                if (checkBool && value is bool)
                {
                    bool.TryParse(value.ToString(), out bool boolResult);

                    result = boolResult == false;
                }

                return result;
            });

            return result;
        }

        public static PropertyInfo[] GetProperties(object obj)
        {
            return obj.GetType().GetProperties();
        }
    }
}
