using System.Collections.Generic;
using System.Threading.Tasks;
using JamboPay.Models;
using Microsoft.EntityFrameworkCore;

namespace JamboPay.Repository
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly AppDbContext _dbContext;

        public ServiceRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddService(Service service)
        {
            _dbContext.Services.Add(service);
        }

        public IEnumerable<Service> FetchServices() => _dbContext.Services.Include(t => t.Transactions);

        public async Task<Service> FetchService(string serviceId) =>
            await _dbContext.Services.FirstOrDefaultAsync(s => s.Id == serviceId);

        public async Task<bool> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }
        
    }
}