using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Oblig2.Models.Entities
{
    public class Blog : IAuthorizationEntity
    {
        public ApplicationUser Owner { get; set; }
        public int BlogId { get; set; }
        public string UserName { get; set; }
        public string CreationDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        // Navigational properties
        public virtual List<Post> Posts { get; set; }

        public virtual ICollection<BlogApplicationUser> Subscribers { get; set; }
    }
}
