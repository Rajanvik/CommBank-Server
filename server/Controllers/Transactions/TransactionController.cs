using Microsoft.AspNetCore.Mvc;
using CommBank.Models;
using CommBank.Services;

namespace CommBank.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionController : ControllerBase
{
    private readonly ITransactionsService _transactionsService;

    public TransactionController(ITransactionsService transactionsService) =>
        _transactionsService = transactionsService;

    // ==========================================
    // 1. READ (All Transactions) -> GET: api/Transaction
    // Database se sabhi Transactions ki list fetch karta hai
    // ==========================================
    [HttpGet]
    public async Task<List<Transaction>> Get() =>
        await _transactionsService.GetAsync();

    // ==========================================
    // 2. READ (Transactions for a User) -> GET: api/Transaction/User/{id}
    // Kisi specific User ke sabhi Transactions fetch karta hai
    // ==========================================
    [HttpGet("User/{id:length(24)}")]
    public async Task<List<Transaction>?> GetForUser(string id) =>
    await _transactionsService.GetForUserAsync(id);

    // ==========================================
    // 3. READ (Single Transaction by ID) -> GET: api/Transaction/{id}
    // Kisi specific Transaction ko uski 24-character ObjectId se fetch karta hai
    // ==========================================
    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Transaction>> Get(string id)
    {
        var transaction = await _transactionsService.GetAsync(id);

        if (transaction is null)
        {
            return NotFound();
        }

        return transaction;
    }

    // ==========================================
    // 4. CREATE -> POST: api/Transaction
    // Naya Transaction create karta hai
    // ==========================================
    [HttpPost]
    public async Task<IActionResult> Post(Transaction newTransaction)
    {
        await _transactionsService.CreateAsync(newTransaction);

        return CreatedAtAction(nameof(Get), new { id = newTransaction.Id }, newTransaction);
    }

    // ==========================================
    // 5. UPDATE -> PUT: api/Transaction/{id}
    // Existing Transaction ko update karta hai
    // ==========================================
    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Transaction updatedTransaction)
    {
        var transaction = await _transactionsService.GetAsync(id);

        if (transaction is null)
        {
            return NotFound();
        }

        updatedTransaction.Id = transaction.Id;

        await _transactionsService.UpdateAsync(id, updatedTransaction);

        return NoContent();
    }

    // ==========================================
    // 6. DELETE -> DELETE: api/Transaction/{id}
    // Transaction ko database se permanently remove karta hai
    // ==========================================
    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var transaction = await _transactionsService.GetAsync(id);

        if (transaction is null)
        {
            return NotFound();
        }

        await _transactionsService.RemoveAsync(id);

        return NoContent();
    }
}