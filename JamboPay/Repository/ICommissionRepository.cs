using System.Threading.Tasks;
using JamboPay.Models;

namespace JamboPay.Repository
{
    public interface ICommissionRepository
    {
        public void AddCommision(Commission commission);
        public Task<bool> SaveChangesAsync();
    }
}