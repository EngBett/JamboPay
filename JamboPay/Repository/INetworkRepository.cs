using System.Collections.Generic;
using System.Threading.Tasks;
using JamboPay.Models;

namespace JamboPay.Repository
{
    public interface INetworkRepository
    {
        public void AddNetwork(Network network);
        public IEnumerable<Network> GetNetworks();
        public Task<Network> GetNetwork(string networkKey);
        public Task<bool> SaveChangesAsync();
    }
}