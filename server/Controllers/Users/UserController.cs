using Microsoft.AspNetCore.Mvc;
using CommBank.Services;
using CommBank.Models;

namespace CommBank.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUsersService _usersService;

    public UserController(IUsersService usersService) =>
        _usersService = usersService;

    // ==========================================
    // 1. READ (All Users) -> GET: api/User
    // Database se sabhi Users ki list fetch karta hai
    // ==========================================
    [HttpGet]
    public async Task<List<User>> Get() =>
        await _usersService.GetAsync();

    // ==========================================
    // 2. READ (Single User by ID) -> GET: api/User/{id}
    // Kisi specific User ko uski 24-character ObjectId se fetch karta hai
    // ==========================================
    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<User>> Get(string id)
    {
        var user = await _usersService.GetAsync(id);

        if (user is null)
        {
            return NotFound();
        }

        return user;
    }

    // ==========================================
    // 3. CREATE -> POST: api/User
    // Naya User database me register / create karta hai
    // ==========================================
    [HttpPost]
    public async Task<IActionResult> Post(User newUser)
    {
        await _usersService.CreateAsync(newUser);

        return CreatedAtAction(nameof(Get), new { id = newUser.Id }, newUser);
    }

    // ==========================================
    // 4. UPDATE -> PUT: api/User/{id}
    // Existing User ki details ko update karta hai
    // ==========================================
    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, User updatedUser)
    {
        var user = await _usersService.GetAsync(id);

        if (user is null)
        {
            return NotFound();
        }

        updatedUser.Id = user.Id;

        await _usersService.UpdateAsync(id, updatedUser);

        return NoContent();
    }

    // ==========================================
    // 5. DELETE -> DELETE: api/User/{id}
    // User ko database se permanently remove karta hai
    // ==========================================
    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var user = await _usersService.GetAsync(id);

        if (user is null)
        {
            return NotFound();
        }

        await _usersService.RemoveAsync(id);

        return NoContent();
    }
}