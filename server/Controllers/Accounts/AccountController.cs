using Microsoft.AspNetCore.Mvc;
using CommBank.Services;
using CommBank.Models;

namespace CommBank.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAccountsService _accountsService;

    public AccountController(IAccountsService accountsService) =>
        _accountsService = accountsService;

    // ==========================================
    // 1. READ (All Accounts) -> GET: api/Account
    // Database se sabhi Accounts ki list fetch karta hai
    // ==========================================
    [HttpGet]
    public async Task<List<Account>> Get() =>
        await _accountsService.GetAsync();

    // ==========================================
    // 2. READ (Single Account by ID) -> GET: api/Account/{id}
    // Kisi specific Account ko uski 24-character ObjectId se fetch karta hai
    // ==========================================
    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Account>> Get(string id)
    {
        var account = await _accountsService.GetAsync(id);

        if (account is null)
        {
            return NotFound();
        }

        return account;
    }

    // ==========================================
    // 3. CREATE -> POST: api/Account
    // Naya Account database me create karta hai
    // ==========================================
    [HttpPost]
    public async Task<IActionResult> Post(Account newAccount)
    {
        await _accountsService.CreateAsync(newAccount);

        return CreatedAtAction(nameof(Get), new { id = newAccount.Id }, newAccount);
    }

    // ==========================================
    // 4. UPDATE -> PUT: api/Account/{id}
    // Existing Account ko nayi details ke sath update karta hai
    // ==========================================
    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Account updatedAccount)
    {
        var account = await _accountsService.GetAsync(id);

        if (account is null)
        {
            return NotFound();
        }

        updatedAccount.Id = account.Id;

        await _accountsService.UpdateAsync(id, updatedAccount);

        return NoContent();
    }

    // ==========================================
    // 5. DELETE -> DELETE: api/Account/{id}
    // Account ko database se permanently remove karta hai
    // ==========================================
    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var account = await _accountsService.GetAsync(id);

        if (account is null)
        {
            return NotFound();
        }

        await _accountsService.RemoveAsync(id);

        return NoContent();
    }
}