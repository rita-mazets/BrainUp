using BrainUp.Models;
using System.ComponentModel.DataAnnotations;

namespace BrainUp.ViewModels
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "No email specified")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Not correct Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "No password specified")]
        [MinLength(7, ErrorMessage = "Lenth must be more than 7")]
        [RegularExpression(@"((?=.*\d)(?=.*[a-z])(?=.*[\W]).{6,20})", ErrorMessage = "Password must contain one digit from 0-9, one lowercase charactersone lowercase characters, least one special character")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password was entered incorrectly")]
        public string ConfirmPassword { get; set; }

        public User ToUser()
        {
            User user = new();
            user.Email = Email;
            user.PasswordHash = Password;

            return user;
        }
    }
}
