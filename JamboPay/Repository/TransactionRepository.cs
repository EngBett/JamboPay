using System.Collections.Generic;
using System.Threading.Tasks;
using JamboPay.Models;
using Microsoft.EntityFrameworkCore;

namespace JamboPay.Repository
{
    public class TransactionRepository:ITransactionRepository
    {
        private AppDbContext _dbContext;

        public TransactionRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Transaction> FetchTransactions() => _dbContext.Transactions.Include(c=>c.Commission).ThenInclude(a=>a.ApplicationUser).Include(s=>s.Service).Include(a=>a.ApplicationUser);

        public Transaction AddTransaction(Transaction transaction)
        {
            _dbContext.Add(transaction);
            return transaction;
        }
        public async Task<bool> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}