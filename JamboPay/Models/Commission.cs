using System;

namespace JamboPay.Models
{
    public class Commission
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public double Amount { get; set; }
        public string TransactionId { get; set; }
        public Transaction Transaction { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}