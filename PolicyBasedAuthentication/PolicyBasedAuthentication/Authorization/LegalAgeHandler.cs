using Microsoft.AspNetCore.Authorization;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PolicyBasedAuthorization.Authorization
{
    public class LegalAgeRequirement: IAuthorizationRequirement { }

    public class LegalAgeHandler : AuthorizationHandler<LegalAgeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, LegalAgeRequirement requirement)
        {
            if(!context.User.HasClaim(c => c.Type == ClaimTypes.DateOfBirth))
            {
                return Task.CompletedTask;
            }

            var dateOfBirth = Convert.ToDateTime(context.User.FindFirst(c => c.Type == ClaimTypes.DateOfBirth).Value);

            int calculatedAge = DateTime.Today.Year - dateOfBirth.Year;
            if (dateOfBirth > DateTime.Today.AddYears(-calculatedAge))
            {
                calculatedAge--;
            }

            if(calculatedAge >= 18)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;

        }
    }
}
