using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace Authorization
{
    public class ResourceBasedAuthorizationHandler : AuthorizationHandler<ResourceBasedRequirement, int>
    {
        private readonly IUsersRepository _userRpository;

        public ResourceBasedAuthorizationHandler(IUsersRepository userRpository)
        {
            _userRpository = userRpository;
        }
        
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceBasedRequirement requirement, int resource)
        {
            var hasAccess = _userRpository.HasAccess(resource);
            if(hasAccess)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            } 
            return Task.CompletedTask;
        }
    }
}
