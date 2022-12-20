using BrainUp.Data;
using BrainUp.Models;
using BrainUp.StoredProcedure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BrainUp.Components.SubmenuActions;
using BrainUp.ViewModels;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace BrainUp.Components.OptionActions
{
    public class ReadOptionsViewComponent: ViewComponent
    {
        private readonly BrainUpBdContext _context;
        private readonly IWebHostEnvironment _environment;

        public ReadOptionsViewComponent(BrainUpBdContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public IViewComponentResult Invoke(int taskId)
        {
            var options = _context.Options.Where(x => x.TaskId == taskId).ToList();

            List<OptionViewModel> list = new();

            foreach (var option in options)
            {
                OptionViewModel model = new();
                model.FromOption(option);
                list.Add(model);
            }

            ViewData["TaskId"] = taskId;

            return View(list);
        }
    }
}
