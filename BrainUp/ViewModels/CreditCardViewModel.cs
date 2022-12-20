using System.ComponentModel.DataAnnotations;

namespace BrainUp.ViewModels
{
    public class CreditCardViewModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "No card number specified")]
        [StringLength(19, ErrorMessage = "The number length must be 19")]
        [Display(Name = "Card Number")]
        //[RegularExpression(@"^\d{4} \d{4} \d{4} \d{4}$", ErrorMessage = "You must enter only digit")]
        public string Number { get; set; }

        [Required(ErrorMessage = "No CVV specified")]
        [StringLength(3, ErrorMessage = "The number length must be 3")]
        [RegularExpression(@"^\d{3}$", ErrorMessage = "You must enter only digit")]
        public string CVV { get; set; }

        [Required(ErrorMessage = "No expiration date specified")]
        [Display(Name = "Expiration date")]
        [DataType(DataType.Date)]
        public DateTime ExpirationDate { get; set; }

        public double? Balance { get; set; }

        [Display(Name = "Currency")]
        public string? CurrencySymbol { get; set; } = null!;

        [Required(ErrorMessage = "No owner specified")]
        public string Owner { get; set; }

        public int UserId { get; set; }
    }
}
