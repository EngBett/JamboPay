using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JamboPay.Models;
using Microsoft.EntityFrameworkCore;

namespace JamboPay.Repository
{
    public class UserNetworkRepository : IUserNetworkRepository
    {
        private readonly AppDbContext _dbContext;

        public UserNetworkRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void AddUserNetwork(UserNetwork userNetwork)
        {
            _dbContext.UserNetworks.Add(userNetwork);
        }

        public IEnumerable<UserNetwork> GetSupporters() => _dbContext.UserNetworks;

        public UserNetwork GetUserNetwork(string userId) => _dbContext.UserNetworks.Include(n => n.Network)
            .FirstOrDefault(u => u.ApplicationUserId == userId);

        public async Task<bool> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}