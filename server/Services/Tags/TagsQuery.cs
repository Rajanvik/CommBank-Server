using CommBank.Models;
using MongoDB.Driver;

namespace CommBank.Services;

public partial class TagsService
{
    // ==========================================
    // QUERIES (GET Operations - Read Data)
    // ==========================================
    public async Task<List<Tag>> GetAsync() =>
        await _tagsCollection.Find(_ => true).ToListAsync();

    public async Task<Tag?> GetAsync(string id) =>
        await _tagsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
}
