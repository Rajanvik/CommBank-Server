using CommBank.Models;
using MongoDB.Driver;

namespace CommBank.Services;

public partial class TransactionsService : ITransactionsService
{
    private readonly IMongoCollection<Transaction> _transactionsCollection;

    public TransactionsService(IMongoDatabase mongoDatabase)
    {
        _transactionsCollection = mongoDatabase.GetCollection<Transaction>("Transactions");
    }

    // ==========================================
    // MUTATIONS (POST / PUT / DELETE - Write Data)
    // ==========================================
    public async Task CreateAsync(Transaction newTransaction) =>
        await _transactionsCollection.InsertOneAsync(newTransaction);

    public async Task UpdateAsync(string id, Transaction updatedTransaction) =>
        await _transactionsCollection.ReplaceOneAsync(x => x.Id == id, updatedTransaction);

    public async Task RemoveAsync(string id) =>
        await _transactionsCollection.DeleteOneAsync(x => x.Id == id);
}
