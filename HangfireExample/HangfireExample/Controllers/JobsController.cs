using Hangfire;
using HangfireExample.Jobs;
using Microsoft.AspNetCore.Mvc;

namespace HangfireExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        [HttpPost]
        public IActionResult StartJob([FromServices] IBackgroundJobClient backgroundJobs)
        {
            backgroundJobs.Enqueue(() => Console.WriteLine("Hello world from Hangfire!"));
            backgroundJobs.Enqueue<SomeJob>(j => j.Execute());
            backgroundJobs.Enqueue<SomeJobWithException>(j => j.Execute());

            return NoContent();
        }

    }
}
