using BrainUp.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.ComponentModel.DataAnnotations;

namespace BrainUp.ViewModels
{
    public class PriceModel
    {
        private decimal price;

        [Display(Name = "Is paid")]
        public bool? IsPaid { get; set; }

        [Display(Name = "Price")]
        [RegularExpression("^\\d+(,\\d{1,2})?$", ErrorMessage = "Cource must be more or equal 0 and has seporator \' ,\' ")]
        public string? PriceString { get; set; }

        [Display(Name = "Price")]
        public decimal? Price 
        {
            get
            {
                if (PriceString is null)
                {
                    price = 0;
                }
                else
                {
                    price = decimal.Parse(PriceString);
                }

                return price;
            }
            set
            {
                price = value ?? 0;
            } 
        }

        [Display(Name = "Currency")]
        public string? Currency { get; set; }
    }
}
