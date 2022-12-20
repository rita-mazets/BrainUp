using BrainUp.Data;
using BrainUp.Models;
using BrainUp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BrainUp.Components
{
    public class TaskDetailsViewComponent : ViewComponent
    {
        private readonly BrainUpBdContext _context;

        public TaskDetailsViewComponent(BrainUpBdContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int taskId, int myAnswer)
        {
            var task = await _context.Tasks.Include(t => t.Options).FirstOrDefaultAsync(t => t.Id == taskId);
            //var menu = await _context.Menus.FirstOrDefaultAsync(t => t.Id == task.MenuId);

            var model = new TaskViewModel();
            model.TaskId = taskId;
            model.Condition = task.Condition;
            model.Options.AddRange(task.Options);
            model.Point = task.Point;
            model.Image = task.Image;
            //model.CourceId = menu.CourceId;
            model.SubMenuId = task.SubMenuId;
            //model.MenuId = task.MenuId;
            if (myAnswer != 0)
            {
                model.Answer = myAnswer;
            }

            foreach (var option in task.Options)
            {
                option.IsTrue = false;
            }

            return View(model);
        }
    }
}
