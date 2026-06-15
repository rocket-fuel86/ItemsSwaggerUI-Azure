using HW1.Models;
using Microsoft.AspNetCore.Mvc;

namespace HW1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly ItemStore _store;

        public ItemController(ItemStore store) => _store = store;

        [HttpGet]
        public IActionResult GetAll() => Ok(_store.GetAll());

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var item = _store.GetById(id);
            return item is null ? NotFound(new { message = $"Item {id} not found." }) : Ok(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Item dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                return BadRequest(new { message = "Name is required." });

            var item = _store.Add(dto);
            return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, [FromBody] Item dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                return BadRequest(new { message = "Name is required." });

            var item = _store.Update(id, dto);
            return item is null ? NotFound(new { message = $"Item {id} not found." }) : Ok(item);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var deleted = _store.Delete(id);
            return deleted ? NoContent() : NotFound(new { message = $"Item {id} not found." });
        }
    }
}
