using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Oblig2.Models.Entities;

namespace Oblig2.Authorization
{
    public class EntityAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, IAuthorizationEntity>
    {
        public readonly UserManager<ApplicationUser> UserManager;

        public EntityAuthorizationHandler(UserManager<ApplicationUser>
            userManager)
        {
            UserManager = userManager;
        }

        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            OperationAuthorizationRequirement requirement,
            IAuthorizationEntity resource
            ) {
            if (context.User == null || resource == null)
            {
                return Task.CompletedTask;
            }

            if (requirement.Name != Constants.UpdateOperationName &&
                requirement.Name != Constants.DeleteOperationName &&
                requirement.Name != Constants.CreateOperationName &&
                requirement.Name != Constants.ReadOperationName)
            {
                return Task.CompletedTask;
            }

            if (resource.Owner.Id == UserManager.GetUserId(context.User) || context.User.IsInRole("Admin"))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}