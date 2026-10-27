using CommBank.Models;
using MongoDB.Driver;

namespace CommBank.Services;

public partial class UsersService : IUsersService
{
    private readonly IMongoCollection<User> _usersCollection;

    public UsersService(IMongoDatabase mongoDatabase)
    {
        _usersCollection = mongoDatabase.GetCollection<User>("Users");
    }

    // ==========================================
    // MUTATIONS (POST / PUT / DELETE - Write Data)
    // ==========================================
    public async Task CreateAsync(User newUser)
    {
        newUser.Password = BCrypt.Net.BCrypt.HashPassword(newUser.Password);
        await _usersCollection.InsertOneAsync(newUser);
    }

    public async Task UpdateAsync(string id, User updatedUser) =>
        await _usersCollection.ReplaceOneAsync(x => x.Id == id, updatedUser);

    public async Task RemoveAsync(string id) =>
        await _usersCollection.DeleteOneAsync(x => x.Id == id);
}
