using System.ComponentModel.DataAnnotations;

namespace TechnoMarket.Web.Models.Auth
{
    public class LoginInput
    {
        //Sign in için model oluşturduk
        [Required]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }

    }
}
