namespace BoxingGearReview.Models
{
    public class Brand
    {
        public int Id  { get; set; }

        public string Name { get; set; }

        // Relationship with 1:N Equipment

        public ICollection<Equipment> Equipments { get; set; }

    }
}
