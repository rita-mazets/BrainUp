using BrainUp.Data;
using BrainUp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BrainUp.Components.TaskActions
{
    public class ConfirmTaskViewComponent:ViewComponent
    {

        private readonly BrainUpBdContext _context;

        public ConfirmTaskViewComponent(BrainUpBdContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(int id)
        {

            var trueOptions = _context.Options.Where(o => o.TaskId == id && o.IsTrue);
            if (trueOptions.Any())
            {
                ViewData["Confirm"] = true;
            }
            else
            {
                ViewData["Confirm"] = false;
            }


            return View();
        }
    }
}
