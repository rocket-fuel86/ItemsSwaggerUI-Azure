using HW1.Models;
using HW1.Services;
using Microsoft.AspNetCore.Mvc;

namespace HW1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TranslateController : ControllerBase
    {
        private readonly TranslateService _service;

        public TranslateController(TranslateService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Translate([FromBody] TranslateRequest request)
        {
            var translated = await _service.TranslateTextAsync(request.Text, request.ToLanguage);
            return Ok(new { Original = request.Text, Translated = translated });
        }
    }
}
