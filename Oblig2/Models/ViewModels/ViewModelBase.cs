using System.ComponentModel.DataAnnotations;

namespace Oblig2.Models.ViewModels
{
    public abstract class ViewModelBase
    {
        [StringLength(20)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Blog name is required")]
        public string Name { get; set; }
    }
}