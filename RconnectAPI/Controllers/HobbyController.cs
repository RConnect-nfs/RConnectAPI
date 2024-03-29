using Microsoft.AspNetCore.Mvc;
using RconnectAPI.Models;
using RconnectAPI.Services;

namespace RconnectAPI.Controllers;

[Controller]
[Route("api/[controller]")]
public class HobbyController: Controller {
    
    private readonly HobbyService _hobbyService;

    public HobbyController(HobbyService hobbyService) {
        _hobbyService = hobbyService;
    }

    [HttpGet]
    public async Task<List<Hobby>> Get()
    {
        return await _hobbyService.GetAsync();
    }
    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Hobby>> Get(string id)
    {
        var user = await _hobbyService.GetAsync(id);

        if (user is null)
        {
            return NotFound();
        }

        return user;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Hobby newBook)
    {
        await _hobbyService.CreateAsync(newBook);

        return CreatedAtAction(nameof(Get), new { id = newBook.Id }, newBook);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Hobby updatedBook)
    {
        var book = await _hobbyService.GetAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        updatedBook.Id = book.Id;

        await _hobbyService.UpdateAsync(id, updatedBook);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var book = await _hobbyService.GetAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        await _hobbyService.RemoveAsync(id);

        return NoContent();
    }

}