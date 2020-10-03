using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Oblig2.Models.Entities
{
    public class Post
    {
        public IdentityUser Owner { get; set; }
        public int PostId { get; set; }
        public string UserName { get; set; }
        public string CreationDate { get; set; }

        public string Name { get; set; }

        public string Description { get; set; } 
        
        // Navigational properties
        public virtual Blog Blog { get; set; }
        public virtual List<Comment> Comments { get; set; }

        public PostStatus Status { get; set; }
    }

    public enum PostStatus
    {
        Submitted,
        Approved,
        Rejected
    }
}
