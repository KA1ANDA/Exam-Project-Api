namespace ExamProjectApi.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public int? TotalAmount { get; set; }
    }
}
