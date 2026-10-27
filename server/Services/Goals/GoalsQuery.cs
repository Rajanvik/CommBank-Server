using CommBank.Models;
using MongoDB.Driver;

namespace CommBank.Services;

public partial class GoalsService
{
    // ==========================================
    // QUERIES (GET Operations - Read Data)
    // ==========================================
    public async Task<List<Goal>> GetAsync() =>
        await _goalsCollection.Find(_ => true).ToListAsync();

    public async Task<List<Goal>?> GetForUserAsync(string id) =>
        await _goalsCollection.Find(x => x.UserId == id).ToListAsync();

    public async Task<Goal?> GetAsync(string id) =>
        await _goalsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
}
