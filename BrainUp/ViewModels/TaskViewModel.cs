using BrainUp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BrainUp.ViewModels
{
    public class TaskViewModel
    {
        public int Id { get; set; }

        public bool? Confirm { get; set; }

        public string Condition { get; set; } = null!;

        public double? Point { get; set; }

        public string Name { get; set; }

        public string? Image { get; set; }

        public int? MenuId { get; set; }

        public int? CourceId { get; set; }

        public int? SubMenuId { get; set; }

        public OptionViewModel? NewOption { get; set; }

        public List<Option> Options { get; } = new List<Option>();

        public virtual SubMenu? SubMenu { get; set; }

        public Tuple<int, int>? Ids { get; set; }
        
        public int? Answer { get; set; }


        public int? TaskId { get; set; }

        public IFormFile? ImageFormFile { get; set; }

        public async Task<BrainUp.Models.Task> ToTask(IWebHostEnvironment environment)
        {
            var task = new BrainUp.Models.Task();
            task.Id = this.TaskId ?? 0;
            task.Name = this.Name;
            task.SubMenuId = this.SubMenuId;
            task.Condition = this.Condition;
            task.Point = this.Point;
            task.Image = this.Image?? "";

            if (this.ImageFormFile != null)
            {
                var fileName = $"{this.Name.Replace(" ", "")}" + Path.GetExtension(this.ImageFormFile.FileName);
                task.Image = fileName;
                var path = Path.Combine(environment.WebRootPath, "img", fileName);
                using (var fStream = new FileStream(path, FileMode.Create))
                {
                    await this.ImageFormFile.CopyToAsync(fStream);
                }
            }

            return task;
        }

        public async Task<BrainUp.Models.Task> ToTaskAdd(IWebHostEnvironment environment)
        {
            var task = new BrainUp.Models.Task();
            task.Name = this.Name;
            task.SubMenuId = this.SubMenuId;
            task.Condition = this.Condition;
            task.Point = this.Point;
            task.Image = this.Image;

            if (this.ImageFormFile != null)
            {
                var fileName = $"{this.Name.Replace(" ", "")}" + Path.GetExtension(this.ImageFormFile.FileName);
                task.Image = fileName;
                var path = Path.Combine(environment.WebRootPath, "img", fileName);
                using (var fStream = new FileStream(path, FileMode.Create))
                {
                    await this.ImageFormFile.CopyToAsync(fStream);
                }
            }

            return task;
        }

        public async void TaskToTaslViewModel(Models.Task task, IWebHostEnvironment environment)
        {
            this.TaskId = task.Id;
            this.Name = task.Name;
            this.SubMenuId = task.SubMenuId;
            this.Condition = task.Condition;
            this.Point = task.Point;
            this.Image = task.Image;
        }

    }
}
