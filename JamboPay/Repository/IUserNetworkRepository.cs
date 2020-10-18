using System.Collections.Generic;
using System.Threading.Tasks;
using JamboPay.Models;

namespace JamboPay.Repository
{
    public interface IUserNetworkRepository
    {
        public void AddUserNetwork(UserNetwork userNetwork);
        public IEnumerable<UserNetwork> GetSupporters();

        public UserNetwork GetUserNetwork(string userId);
        
        public Task<bool> SaveChangesAsync();
    }
}