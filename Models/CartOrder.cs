namespace ExamProjectApi.Models
{
    public class CartOrder
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public int TotalPrice { get; set; }
    }
}
