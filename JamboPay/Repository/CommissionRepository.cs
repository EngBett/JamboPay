using System.Threading.Tasks;
using JamboPay.Models;

namespace JamboPay.Repository
{
    public class CommissionRepository:ICommissionRepository
    {
        private readonly AppDbContext _dbContext;

        public CommissionRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddCommision(Commission commission)
        {
            _dbContext.Commissions.Add(commission);
        }
        
        public async Task<bool> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }
        
    }
}
