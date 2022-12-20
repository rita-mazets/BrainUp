using BrainUp.Data;
using BrainUp.Models;
using BrainUp.StoredProcedure;
using BrainUp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BrainUp.Components.TaskActions
{
    public class EditTaskViewComponent:ViewComponent
    {
        private readonly BrainUpBdContext _context;

        public EditTaskViewComponent(BrainUpBdContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(int id)
        {
            var task = new TaskViewModel();

            var list = TaskAction.ReadAllMenuWithSubmenuTaskForCource(id, _context);

            Dictionary<int, string> dict = new();

            if (list != null)
            {
                foreach (var item in list)
                {
                    int itemId = item.TaskId;
                    dict[itemId] = item.MenuName + " | " + item.SubmenuName + " | " + item.TaskName;
                }
            }

            ViewData["TaskMenu"] = new SelectList(dict, "Key", "Value");

            return View(task);
        }
    }
}
