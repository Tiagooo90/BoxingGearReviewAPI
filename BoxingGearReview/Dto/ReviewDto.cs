public class ReviewDto
{
    public int Id { get; set; }
    public decimal Rating { get; set; }
    public string Comment { get; set; }
    public DateTime Date { get; set; }
    public int UserId { get; set; }
    public int EquipmentId { get; set; }
}
