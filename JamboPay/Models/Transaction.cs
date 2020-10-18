using System;

namespace JamboPay.Models
{
    public class Transaction
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        
        public string ServiceId { get; set; }
        public Service Service { get; set; }

        public double Cost { get; set; }
        
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        
        public Commission Commission { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}