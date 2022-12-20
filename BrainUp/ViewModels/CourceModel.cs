using BrainUp.Attributes;
using BrainUp.Data;
using BrainUp.Models;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Task = BrainUp.Models.Task;

namespace BrainUp.ViewModels
{
    public class CourceModel
    {

        private static DateTime start = DateTime.Now;
        public int Id { get; set; }

        public string Name { get; set; }

        [Display(Name = "Category")]
        public string? Category { get; set; }

        [DataType(DataType.Date)]
        [CurrentDate]
        public DateTime? StartDate
        {
            get
            {
                return start;
            }
            set
            {
                start = value ?? DateTime.Now;
            } 
        }

        [Display(Name = "End date")]
        [DataType(DataType.Date)]
        [CurrentDate]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Short discription")]
        public string? ShotDiscription { get; set; }

        [Display(Name = "Discription")]
        public string? Discription { get; set; }

        [Display(Name = "Storage link")]
        public string? StorageLink { get; set; }

        public IFormFile? Image { get; set; }


        [Display(Name = "Level")]
        public string? Level { get; set; }

        [Display(Name = "Language")]
        public string? Language { get; set; }

        public int? PriceId { get; set; }
        public int? TeacherId { get; set; }

        public virtual PriceModel? Price { get; set; } = new()!;

        public virtual ICollection<Task>? Tasks { get; } = new List<Task>();

        public virtual User? Teacher { get; set; } 
        public string? ImageString { get; set; }

        public FileStream? ImageFile { get; set; }

        public List<Menu>? Menus { get; set; } = new List<Menu>();

        private static string StaticStringStartDate 
        { 
            get 
            {
                    return start.ToShortDateString();
            }
        }

        public void Copy(Cource cource)
        { 
            this.Id = cource.Id;
            this.Name = cource.Name;
            this.Discription = cource.Discription;
            this.ShotDiscription = cource.ShotDiscription;
            this.StartDate = cource.StartDate;
            this.EndDate = cource.EndDate;
            this.Category = cource.Category.Name;
            this.Level = cource.Level.Name;
            this.Language = cource.Language.Name;
            this.StorageLink = cource.StorageLink;
            this.PriceId = cource.PriceId;
            this.Price.Price = cource.Price.Price1;
            this.Price.Currency = cource.Price.CurrencySymbol;
            this.ImageString = cource.Image;
            this.TeacherId = cource.TeacherId;
        }

        public async Task<Cource> CopyToCource(IWebHostEnvironment environment)
        {
            Cource cource = new();

            cource.Id = this.Id;
            cource.Name = this.Name;
            cource.Discription = this.Discription;
            cource.ShotDiscription = this.ShotDiscription;
            cource.StartDate = this.StartDate;
            cource.EndDate = this.EndDate;
            cource.StorageLink = this.StorageLink;
            cource.PriceId = (int)this.PriceId;
            cource.Image = this.ImageString;
            cource.TeacherId = (int)this.TeacherId;

            if (this.Image != null)
            {
                var fileName = $"{this.Name.Replace(" ", "")}" + $"_{this.Teacher}" + $"{DateTime.Now.Ticks}" + Path.GetExtension(this.Image.FileName);
                cource.Image = fileName;
                var path = Path.Combine(environment.WebRootPath, "img", fileName);
                using (var fStream = new FileStream(path, FileMode.Create))
                {
                    await this.Image.CopyToAsync(fStream);
                }
            }

            return cource;
        }
    }
}
