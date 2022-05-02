using DrawLots.Models;

namespace DrawLots
{
    public class LotsService
    {
        public static List<Lots> LotsData = new List<Lots>
            {
                new Lots{獎項 = "一獎", 人數 = 5},
                new Lots{獎項 = "二獎", 人數 = 10}
            };
    }
}
