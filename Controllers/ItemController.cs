using HW1.Models;
using HW1.Services;
using Microsoft.AspNetCore.Mvc;

namespace HW1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly ItemService _store;

        public ItemController(ItemService store) => _store = store;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(
                await _store.GetAll()
            );
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = _store.GetById(id);
            return item is null ? NotFound(new { message = $"Item {id} not found." }) : Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ItemDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                return BadRequest("Name is required");

            var item = await _store.Add(dto);

            return Ok(item);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] ItemDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                return BadRequest(new { message = "Name is required." });

            var item = await _store.Update(id, dto);
            return item is null ? NotFound(new { message = $"Item {id} not found." }) : Ok(item);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _store.Delete(id);
            return deleted ? NoContent() : NotFound(new { message = $"Item {id} not found." });
        }
    }
}
