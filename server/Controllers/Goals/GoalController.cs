using Microsoft.AspNetCore.Mvc;
using CommBank.Services;
using CommBank.Models;

namespace CommBank.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GoalController : ControllerBase
{
    private readonly IGoalsService _goalsService;
    private readonly IUsersService _usersService;

    public GoalController(IGoalsService goalsService, IUsersService usersService)
    {
        _goalsService = goalsService;
        _usersService = usersService;
    }

    // ==========================================
    // 1. READ (All Goals) -> GET: api/Goal
    // Database se sabhi Goals ki list fetch karta hai
    // ==========================================
    [HttpGet]
    public async Task<List<Goal>> Get() =>
        await _goalsService.GetAsync();

    // ==========================================
    // 2. READ (Single Goal by ID) -> GET: api/Goal/{id}
    // Kisi specific Goal ko uski 24-character ObjectId se fetch karta hai
    // ==========================================
    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Goal>> Get(string id)
    {
        var goal = await _goalsService.GetAsync(id);

        if (goal is null)
        {
            return NotFound();
        }

        return goal;
    }

    // ==========================================
    // 3. READ (Goals for a User) -> GET: api/Goal/User/{id}
    // Kisi specific User ID se jude saare Goals fetch karta hai
    // ==========================================
    [HttpGet("User/{id:length(24)}")]
    public async Task<List<Goal>?> GetForUser(string id) =>
        await _goalsService.GetForUserAsync(id);

    // ==========================================
    // 4. CREATE -> POST: api/Goal
    // Naya Goal create karta hai aur us User ke account me link karta hai
    // ==========================================
    [HttpPost]
    public async Task<IActionResult> Post(Goal newGoal)
    {
        // Step A: Naya Goal database me insert karta hai
        await _goalsService.CreateAsync(newGoal);

        // Step B: User ke record me is Goal ki ID add/update karta hai
        if (newGoal.Id is not null && newGoal.UserId is not null)
        {
            var user = await _usersService.GetAsync(newGoal.UserId);

            if (user is not null && user.Id is not null)
            {
                if (user.GoalIds is not null)
                {
                    user.GoalIds.Add(newGoal.Id);
                }
                else
                {
                    user.GoalIds = new()
                    {
                        newGoal.Id
                    };
                }

                await _usersService.UpdateAsync(user.Id, user);
            }
        }

        // Step C: HTTP 201 Created status aur naya Goal return karta hai
        return CreatedAtAction(nameof(Get), new { id = newGoal.Id }, newGoal);
    }

    // ==========================================
    // 5. UPDATE -> PUT: api/Goal/{id}
    // Existing Goal ko nayi values ke sath update karta hai
    // ==========================================
    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Goal updatedGoal)
    {
        // Step A: Check karta hai ki Goal database me exist karta hai ya nahi
        var goal = await _goalsService.GetAsync(id);

        if (goal is null)
        {
            return NotFound();
        }

        // Step B: Same ID assign karke database me update kar deta hai
        updatedGoal.Id = goal.Id;

        await _goalsService.UpdateAsync(id, updatedGoal);

        return NoContent(); // HTTP 204 Success
    }

    // ==========================================
    // 6. DELETE -> DELETE: api/Goal/{id}
    // Goal ko database se permanently remove karta hai
    // ==========================================
    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        // Step A: Check karta hai ki Goal exist karta hai ya nahi
        var goal = await _goalsService.GetAsync(id);

        if (goal is null)
        {
            return NotFound();
        }

        // Step B: Database se remove karta hai
        await _goalsService.RemoveAsync(id);

        return NoContent(); // HTTP 204 Success
    }
}