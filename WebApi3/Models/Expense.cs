﻿namespace WebApi3.Models
{
    public class Expense
    {
        public int? Id { get; set; }
        public string? Title { get; set; }
        public decimal? Amount { get; set; }
        public string? Category { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? UserId { get; set; }
       
    }
}
