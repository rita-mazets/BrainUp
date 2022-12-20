using BrainUp.Data;
using BrainUp.Models;
using BrainUp.StoredProcedure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BrainUp.Components.SubmenuActions;
using BrainUp.ViewModels;

namespace BrainUp.Components.TaskActions
{
    public class ReadTaskViewComponent: ViewComponent
    {
        private readonly BrainUpBdContext _context;
        private readonly IWebHostEnvironment _environment;

        public ReadTaskViewComponent(BrainUpBdContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public IViewComponentResult Invoke(int taskId, int courceId)
        {
            var tasks = TaskAction.Read(taskId, _context);

            TaskViewModel taskModel = new TaskViewModel();

            if (tasks != null && tasks.Any())
            {
                taskModel.TaskToTaslViewModel(tasks.First(), _environment);
            }

            taskModel.CourceId = courceId;

            return View(taskModel);
        }
    }
}
