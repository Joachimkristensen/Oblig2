using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Oblig2.Models.Entities;

namespace Oblig2.Models.ViewModels
{
    public class PostEditViewModel
    {
        public virtual IdentityUser Owner { get; set; }

        public int PostId { get; set; }

        [StringLength(20)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        public string UserName { get; set; }
        public string CreationDate { get; set; }

        // Navigational properties
        public int BlogId { get; set; }
        public List<Comment> Comments { get; set; }
    }
}