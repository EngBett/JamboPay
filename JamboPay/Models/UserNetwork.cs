using System;
using System.Collections.Generic;

namespace JamboPay.Models
{
    public class UserNetwork
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string NetworkId { get; set; }
        public Network Network { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}