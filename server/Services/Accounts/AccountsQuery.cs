using CommBank.Models;
using MongoDB.Driver;

namespace CommBank.Services;

public partial class AccountsService
{
    // ==========================================
    // QUERIES (GET Operations - Read Data)
    // ==========================================
    public async Task<List<Account>> GetAsync() =>
        await _accountsCollection.Find(_ => true).ToListAsync();

    public async Task<Account?> GetAsync(string id) =>
        await _accountsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
}
