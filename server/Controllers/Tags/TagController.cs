using Microsoft.AspNetCore.Mvc;
using CommBank.Services;

namespace CommBank.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TagController : ControllerBase
{
    private readonly ITagsService _tagsService;

    public TagController(ITagsService tagsService) =>
        _tagsService = tagsService;

    // ==========================================
    // 1. READ (All Tags) -> GET: api/Tag
    // Database se sabhi Tags ki list fetch karta hai
    // ==========================================
    [HttpGet]
    public async Task<List<CommBank.Models.Tag>> Get() =>
        await _tagsService.GetAsync();

    // ==========================================
    // 2. READ (Single Tag by ID) -> GET: api/Tag/{id}
    // Kisi specific Tag ko uski 24-character ObjectId se fetch karta hai
    // ==========================================
    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<CommBank.Models.Tag>> Get(string id)
    {
        var tag = await _tagsService.GetAsync(id);

        if (tag is null)
        {
            return NotFound();
        }

        return tag;
    }

    // ==========================================
    // 3. CREATE -> POST: api/Tag
    // Naya Tag database me create karta hai
    // ==========================================
    [HttpPost]
    public async Task<IActionResult> Post(CommBank.Models.Tag newTag)
    {
        await _tagsService.CreateAsync(newTag);

        return CreatedAtAction(nameof(Get), new { id = newTag.Id }, newTag);
    }

    // ==========================================
    // 4. UPDATE -> PUT: api/Tag/{id}
    // Existing Tag ko nayi details ke sath update karta hai
    // ==========================================
    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, CommBank.Models.Tag updatedTag)
    {
        var tag = await _tagsService.GetAsync(id);

        if (tag is null)
        {
            return NotFound();
        }

        updatedTag.Id = tag.Id;

        await _tagsService.UpdateAsync(id, updatedTag);

        return NoContent();
    }

    // ==========================================
    // 5. DELETE -> DELETE: api/Tag/{id}
    // Tag ko database se permanently remove karta hai
    // ==========================================
    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var tag = await _tagsService.GetAsync(id);

        if (tag is null)
        {
            return NotFound();
        }

        await _tagsService.RemoveAsync(id);

        return NoContent();
    }
}