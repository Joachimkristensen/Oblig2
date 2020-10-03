using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Oblig2.Models.Entities;

namespace Oblig2.Models.ViewModels
{
    public class BlogEditViewModel
    {
        public virtual IdentityUser Owner { get; set; } 

        public int BlogId { get; set; }

        [StringLength(20)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Blog name is required")]
        public string Name { get; set; }

        public string UserName { get; set; }
        public string CreationDate { get; set; }

        public List<Post> Posts { get; set; }
    }
}
