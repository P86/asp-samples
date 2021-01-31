using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace AtionFilters
{
    public class GlobalActionFilter : IActionFilter
    {
        private readonly ILogger<GlobalActionFilter> logger;

        public GlobalActionFilter(ILogger<GlobalActionFilter> logger)
        {
            this.logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            logger.LogTrace($"OnActionExecuting in {nameof(GlobalActionFilter)}");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            logger.LogTrace($"OnActionExecuted in {nameof(GlobalActionFilter)}");
        }
    }
}
