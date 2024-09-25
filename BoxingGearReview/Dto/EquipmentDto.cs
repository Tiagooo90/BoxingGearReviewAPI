namespace BoxingGearReview.Dto
{
    public class EquipmentDto
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Material { get; set; }

        public decimal Price { get; set; }

        public byte[] Image { get; set; }

        public int CategoryId { get; set; }
        public int BrandId { get; set; }


        public List<ReviewDto> Reviews { get; set; }

        public BrandDto Brand { get; set; }  // Propriedade Brand adicionada
        public CategoryDto Category { get; set; }



    }
}
