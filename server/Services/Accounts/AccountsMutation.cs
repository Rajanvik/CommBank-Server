using CommBank.Models;
using MongoDB.Driver;

namespace CommBank.Services;

public partial class AccountsService : IAccountsService
{
    private readonly IMongoCollection<Account> _accountsCollection;

    public AccountsService(IMongoDatabase mongoDatabase)
    {
        _accountsCollection = mongoDatabase.GetCollection<Account>("Accounts");
    }

    // ==========================================
    // MUTATIONS (POST / PUT / DELETE - Write Data)
    // ==========================================
    public async Task CreateAsync(Account newAccount) =>
        await _accountsCollection.InsertOneAsync(newAccount);

    public async Task UpdateAsync(string id, Account updatedAccount) =>
        await _accountsCollection.ReplaceOneAsync(x => x.Id == id, updatedAccount);

    public async Task RemoveAsync(string id) =>
        await _accountsCollection.DeleteOneAsync(x => x.Id == id);
}
