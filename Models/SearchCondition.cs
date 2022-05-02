using System.ComponentModel.DataAnnotations.Schema;

namespace DrawLots.Models
{
    public class SearchCondition
    {
        [NotMapped]
        public bool IsFromSearch { get; set; }
    }
}
