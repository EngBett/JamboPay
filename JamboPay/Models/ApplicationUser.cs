using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JamboPay.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        
        public Network Network { get; set; }
        public IEnumerable<UserNetwork> UserNetworks { get; set; }
        public IEnumerable<Transaction> Transactions { get; set; }
        public IEnumerable<Commission> Commissions { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
