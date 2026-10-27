using CommBank.Models;
using MongoDB.Driver;

namespace CommBank.Services;

public partial class TagsService : ITagsService
{
    private readonly IMongoCollection<Tag> _tagsCollection;

    public TagsService(IMongoDatabase mongoDatabase)
    {
        _tagsCollection = mongoDatabase.GetCollection<Tag>("Tags");
    }

    // ==========================================
    // MUTATIONS (POST / PUT / DELETE - Write Data)
    // ==========================================
    public async Task CreateAsync(Tag newTag) =>
        await _tagsCollection.InsertOneAsync(newTag);

    public async Task UpdateAsync(string id, Tag updatedTag) =>
        await _tagsCollection.ReplaceOneAsync(x => x.Id == id, updatedTag);

    public async Task RemoveAsync(string id) =>
        await _tagsCollection.DeleteOneAsync(x => x.Id == id);
}
