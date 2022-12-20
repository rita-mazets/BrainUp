using BrainUp.Models;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace BrainUp.ViewModels
{
    public class LoginModel
    {
        [Required(ErrorMessage = "No email specified")]
        public string Email { get; set; }

        [Required(ErrorMessage = "No password specified")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        public User ToUser()
        {
            User user = new();
            user.Email = Email;
            user.PasswordHash = Password;

            return user;
        }
    }
}
