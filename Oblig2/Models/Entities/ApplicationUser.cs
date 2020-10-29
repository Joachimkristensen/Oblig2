using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Oblig2.Models.Entities
{
    
    public class ApplicationUser : IdentityUser
    {
        
        public virtual List<BlogApplicationUser> Blogs { get; set; }
    }
}