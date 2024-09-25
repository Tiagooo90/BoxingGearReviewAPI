namespace BoxingGearReview.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        // Relationship with 1:N Equipment

        public ICollection<Equipment> Equipments { get; set; }
    }
}
