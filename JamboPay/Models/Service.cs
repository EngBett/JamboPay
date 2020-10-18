using System;
using System.Collections.Generic;

namespace JamboPay.Models
{
    public class Service
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public double CommissionPercentage { get; set; }
        public IEnumerable<Transaction> Transactions { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}