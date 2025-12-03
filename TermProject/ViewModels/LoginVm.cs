using System.ComponentModel.DataAnnotations;

namespace TermProject.ViewModels
{
    public class LoginVm
    {
        [Required] public string UserName { get; set; }
        [Required] public string Password { get; set; }

    }
}
