namespace BoxingGearReview.Models
{
    public class Review
    {
        public int Id { get; set; }

        public decimal Rating { get; set; }

        public string Comment { get; set; }

        public DateTime Date { get; set; }

        // FK to User

        public int UserId { get; set; }

        public User User { get; set; }

        //FK to Equipment

        public int EquipmentId { get; set; }

        public Equipment Equipment { get; set; }
    }
}
