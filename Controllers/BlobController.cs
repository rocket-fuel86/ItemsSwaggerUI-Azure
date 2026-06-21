using HW1.Services;
using Microsoft.AspNetCore.Mvc;

namespace HW1.Controllers
{
    [Route("files")]
    public class FileController : ControllerBase
    {
        private readonly BlobService _blob;

        public FileController(
            BlobService blob)
        {
            _blob = blob;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            await _blob.Upload(file);

            return Ok();
        }
    }
}
