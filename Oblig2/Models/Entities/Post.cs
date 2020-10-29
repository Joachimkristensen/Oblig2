using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Oblig2.Models.Entities
{
    public class Post : IAuthorizationEntity
    {
        public virtual ApplicationUser Owner { get; set; }
        public int PostId { get; set; }
        public string UserName { get; set; }
        public string CreationDate { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; } 
        
        // Navigational properties
        public virtual Blog Blog { get; set; }

        public int BlogId { get; set; }

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
