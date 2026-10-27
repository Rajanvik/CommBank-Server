using CommBank.Models;
using MongoDB.Driver;

namespace CommBank.Services;

public partial class UsersService
{
    // ==========================================
    // QUERIES (GET Operations - Read Data)
    // ==========================================
    public async Task<List<User>> GetAsync() =>
        await _usersCollection.Find(_ => true).ToListAsync();

    public async Task<User?> GetAsync(string id) =>
        await _usersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
}
