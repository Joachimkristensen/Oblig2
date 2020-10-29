using Microsoft.AspNetCore.Identity;

namespace Oblig2.Models.Entities
{
    public class BlogApplicationUser
    {
        public int BlogApplicationUserId { get; set; }

        public virtual Blog Blog { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}