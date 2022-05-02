namespace DrawLots.Models
{
    public class User : SearchCondition
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ActivityId { get; set; }

    }
}
