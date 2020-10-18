using System.Collections.Generic;
using System.Threading.Tasks;
using JamboPay.Models;
using Microsoft.EntityFrameworkCore;

namespace JamboPay.Repository
{
    public class NetworkRepository : INetworkRepository
    {
        private readonly AppDbContext _dbContext;

        public NetworkRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddNetwork(Network network)
        {
            _dbContext.Networks.Add(network);
        }

        public IEnumerable<Network> GetNetworks() => _dbContext.Networks.Include(a => a.ApplicationUser)
            .Include(u => u.UserNetworks).ThenInclude(a => a.ApplicationUser);

        public async Task<Network> GetNetwork(string networkKey) => await _dbContext.Networks.FirstOrDefaultAsync(n=>n.NetworkKey == networkKey);

        public async Task<bool> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}