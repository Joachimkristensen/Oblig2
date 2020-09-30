using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Oblig2.Models.Entities
{
    public class Post : BlogEntity
    {
        public virtual IdentityUser Owner { get; set; }
        public int PostId { get; set; }
        public string UserName { get; set; }
        public string CreationDate { get; set; }
        
        // Navigational properties
        public string BlogId { get; set; }
        public virtual Blog Blog { get; set; }
        public virtual List<Comment> Comments { get; set; }
    }
}
