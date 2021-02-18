using Microsoft.AspNetCore.Mvc;
using System;

namespace Streaming.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusicController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var fileName = "Assets/Movement.mp3";
            if (!System.IO.File.Exists(fileName))
            {
                throw new InvalidOperationException($"File {fileName} does not exists");
            }

            return File(fileStream: System.IO.File.OpenRead(fileName),
                contentType: "audio/mpeg",
                enableRangeProcessing: true);
        }
    }
}
