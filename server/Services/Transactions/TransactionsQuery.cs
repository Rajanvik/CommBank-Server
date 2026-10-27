using CommBank.Models;
using MongoDB.Driver;

namespace CommBank.Services;

public partial class TransactionsService
{
    // ==========================================
    // QUERIES (GET Operations - Read Data)
    // ==========================================
    public async Task<List<Transaction>> GetAsync() =>
        await _transactionsCollection.Find(_ => true).ToListAsync();

    public async Task<List<Transaction>?> GetForUserAsync(string id) =>
        await _transactionsCollection.Find(x => x.UserId == id).ToListAsync();

    public async Task<Transaction?> GetAsync(string id) =>
        await _transactionsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
}
