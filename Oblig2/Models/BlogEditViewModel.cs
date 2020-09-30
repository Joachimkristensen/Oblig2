using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Oblig2.Models
{
    public class BlogEditViewModel
    {
        [StringLength(20)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Blog name is required")]
        public string Name { get; set; }
    }
}
