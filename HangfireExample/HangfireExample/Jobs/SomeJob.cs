using HangfireExample.Services;

namespace HangfireExample.Jobs
{
    public class SomeJob
    {
        private readonly SomeService service;
        private readonly ILogger<SomeJob> logger;

        public SomeJob(SomeService service, ILogger<SomeJob> logger)
        {
            this.service = service;
            this.logger = logger;
        }
        
        public async Task Execute()
        {
            await Task.Delay(TimeSpan.FromSeconds(10));
            
            var text = service.GetText();
            logger.LogInformation(text);
        }
    }
}
