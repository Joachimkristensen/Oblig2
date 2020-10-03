using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Oblig2.Models.Entities;

namespace Oblig2.Authorization
{
    public class PostIsOwnerAuthorizationHandler
                : AuthorizationHandler<OperationAuthorizationRequirement, Post>
    {
        public readonly UserManager<IdentityUser> UserManager;

        public PostIsOwnerAuthorizationHandler(UserManager<IdentityUser>
            userManager)
        {
            UserManager = userManager;
        }

        protected override Task
            HandleRequirementAsync(AuthorizationHandlerContext context,
                                   OperationAuthorizationRequirement requirement,
                                   Post resource)
        {
            if (context.User == null || resource == null)
            {
                // Return Task.FromResult(0) if targeting a version of
                // .NET Framework older than 4.6:
                return Task.CompletedTask;
            }

            // If we're not asking for CRUD permission, return.

            if (requirement.Name != Constants.UpdateOperationName &&
                requirement.Name != Constants.DeleteOperationName)
            {
                return Task.CompletedTask;
            }

            var ownerId = resource.Owner.Id;

            if (resource.Owner.Id == UserManager.GetUserId(context.User))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}