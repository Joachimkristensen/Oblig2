using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Oblig2.Models.ViewModels
{
    public class CommentViewModel
    {
        public virtual IdentityUser Owner { get; set; }

        public int CommentId { get; set; }

        public int PostId { get; set; }

        [StringLength(20)]
        public string Description { get; set; }

        public string UserName { get; set; }
        public string CreationDate { get; set; }

    }
}
