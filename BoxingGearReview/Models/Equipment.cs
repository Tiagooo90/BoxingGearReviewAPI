using System.Reflection.Metadata;

namespace BoxingGearReview.Models
{
    public class Equipment
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Material { get; set; }

        public decimal Price { get; set; }

        public byte[] Image { get; set; }


        // FK to Category

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        // FK to Brand

        public int BrandId { get; set; }
        public Brand Brand { get; set; }


        // Relationship with 1:N Review
        public ICollection<Review> Reviews { get; set; }
    }
}
