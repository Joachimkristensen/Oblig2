using Microsoft.AspNetCore.Identity;

namespace Oblig2.Models.Entities
{
    public class Comment
    {
        public virtual IdentityUser Owner { get; set; }
        public int CommentId { get; set; }
        public string UserName { get; set; }
        public string CreationDate { get; set; }

        public string Description { get; set; }

        // Navigational properties
        public virtual Post Post { get; set; }
    }
}
