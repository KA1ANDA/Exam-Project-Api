﻿namespace ExamProjectApi.Models
{
    public class OrderDetails
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public int TotalPrice { get; set; }
    }
}
