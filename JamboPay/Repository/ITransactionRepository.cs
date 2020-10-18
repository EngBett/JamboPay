using System.Collections.Generic;
using System.Threading.Tasks;
using JamboPay.Models;

namespace JamboPay.Repository
{
    public interface ITransactionRepository
    {
        public IEnumerable<Transaction> FetchTransactions();
        public Transaction AddTransaction(Transaction transaction);
        public Task<bool> SaveChangesAsync();
    }
}