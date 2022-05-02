namespace DrawLots.Service
{
    public class JsonResultService
    {
        public static object Success
        {
            get
            {
                return new { success = true };
            }
        }

        public static object Fail
        {
            get
            {
                return new { success = false };
            }
        }

        public static object Get(bool success, string message = null, int? id = null)
        {
            return new { success, message, id };
        }
    }
}
