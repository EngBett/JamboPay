using System;
using System.Collections.Generic;

namespace JamboPay.Models
{
    public class Network
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string NetworkKey { get; set; } = Guid.NewGuid().ToString();
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        
        public IEnumerable<UserNetwork> UserNetworks { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}