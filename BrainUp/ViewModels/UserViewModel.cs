using BrainUp.Models;

namespace BrainUp.ViewModels
{
    public class UserViewModel
    {
        public int? Id { get; set; }
        public string? Email { get; set; } = null!;

        public string? PhoneNumber { get; set; }

        public string? Discription { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? CreditCard { get; set; }

        public string? ImageString { get; set; }

        public int? CreditCardId { get; set; }

        public IFormFile? ImageFormFile { get; set; }
    }
}
