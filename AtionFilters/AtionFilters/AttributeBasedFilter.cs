using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace AtionFilters
{
    public class AttributeBasedFilter: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine($"OnActionExecuting in {nameof(AttributeBasedFilter)}");

            base.OnActionExecuting(context);
        }
    }
}
