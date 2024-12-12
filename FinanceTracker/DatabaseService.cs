using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace FinanceTracker
{
    public class DatabaseService
    {
        private readonly SQLiteAsyncConnection _database;

        public DatabaseService(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Transaction>().Wait();
            _database.CreateTableAsync<User>().Wait();
        }

        public Task<List<Transaction>> GetTransactionsAsync()
        {
            return _database.Table<Transaction>().ToListAsync();
        }

        public Task<int> SaveTransactionAsync(Transaction transaction)
        {
            if (transaction.Id != 0)
            {
                return _database.UpdateAsync(transaction);
            }
            else
            {
                return _database.InsertAsync(transaction);
            }
        }

        public Task<int> DeleteTransactionAsync(Transaction transaction)
        {
            return _database.DeleteAsync(transaction);
        }

        public Task<int> SaveUserAsync(User user)
        {
            return _database.InsertAsync(user);
        }

        public Task<User> GetUserAsync(string username, string password)
        {
            return _database.Table<User>().Where(u => u.Username == username && u.Password == password).FirstOrDefaultAsync();
        }

        public Task<User> GetUserByUsernameAsync(string username)
        {
            return _database.Table<User>().Where(u => u.Username == username).FirstOrDefaultAsync();
        }
    }
}
