using System.ComponentModel.DataAnnotations;

namespace Atelier3.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required]
        [Display(Name = "Role")]

        public string RoleName { get; set; }

    }
}
