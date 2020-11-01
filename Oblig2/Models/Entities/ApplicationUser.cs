using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Oblig2.Models.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public virtual List<BlogApplicationUser> Blogs { get; set; }
    }
}