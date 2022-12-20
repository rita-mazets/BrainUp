using BrainUp.Data;
using BrainUp.Models;
using BrainUp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BrainUp.Components.OptionActions
{
    public class CreateOptionViewComponent:ViewComponent
    {
        private readonly BrainUpBdContext _context;

        public CreateOptionViewComponent(BrainUpBdContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(int taskId)
        {
            var model = new OptionViewModel();
            model.TaskId = taskId;

            return View(model);
        }
    }
}
