using Microsoft.AspNetCore.Mvc;
using backend.Models;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoController : ControllerBase
{
    private static List<TodoItem> _items = new List<TodoItem>();
    private static int _nextId = 1;

    [HttpGet]
    public ActionResult<IEnumerable<TodoItem>> Get() => Ok(_items);

    [HttpPost]
    public ActionResult<TodoItem> Create([FromBody] TodoItem item)
    {
        item.Id = _nextId++;
        _items.Add(item);
        return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] TodoItem updatedItem)
    {
        var item = _items.FirstOrDefault(x => x.Id == id);
        if (item == null) return NotFound();

        item.Title = updatedItem.Title;
        item.IsDone = updatedItem.IsDone;

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var item = _items.FirstOrDefault(x => x.Id == id);
        if (item == null) return NotFound();

        _items.Remove(item);
        return NoContent();
    }
}
