using System.Collections.Generic;
using System.Threading.Tasks;
using JamboPay.Models;

namespace JamboPay.Repository
{
    public interface IServiceRepository
    {
        public void AddService(Service service);
        public IEnumerable<Service> FetchServices();

        public Task<Service> FetchService(string serviceId);
        public Task<bool> SaveChangesAsync();
    }
}