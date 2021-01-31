using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace AtionFilters
{
    public class ScopedActionFilter : IActionFilter
    {
        private readonly ILogger<GlobalActionFilter> logger;
        
        public ScopedActionFilter(ILogger<GlobalActionFilter> logger)
        {
            this.logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            logger.LogTrace($"OnActionExecuting in {nameof(ScopedActionFilter)}");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            logger.LogTrace($"OnActionExecuted in {nameof(ScopedActionFilter)}");
        }
    }
}
