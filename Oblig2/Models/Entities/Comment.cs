using Microsoft.AspNetCore.Identity;

namespace Oblig2.Models.Entities
{
    public class Comment : IAuthorizationEntity
    {
        public virtual ApplicationUser Owner { get; set; }
        public string OwnerId { get; set; }
        public int CommentId { get; set; }
        public string UserName { get; set; }
        public string CreationDate { get; set; }

        public string Description { get; set; }

        // Navigational properties
        public virtual Post Post { get; set; }

        public int PostId { get; set; }
    }
}
