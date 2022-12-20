using BrainUp.Data;
using BrainUp.Models;
using BrainUp.StoredProcedure;
using BrainUp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;
using Task = BrainUp.Models.Task;


namespace BrainUp.Controllers
{
    public class TaskController : Controller
    {
        private readonly BrainUpBdContext _context;
        private readonly IWebHostEnvironment _environment;

        public TaskController(BrainUpBdContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Respond(TaskViewModel task)
        {
            if (task == null || task.TaskId == null)
            {
                return NotFound();
            }

            var trueAnswer = await _context.Options.FirstOrDefaultAsync(o => o.TaskId == task.TaskId && o.IsTrue == true);

            bool isTrue;
            if (trueAnswer == null)
            {
                isTrue = false;
                ViewData["NotAnswer"] = "Teacher doesn't have right answer";
                return RedirectToAction("Study", "Cources", new
                {
                    id = (int)task.Id,
                    submenuId = task.SubMenuId,
                    taskId = task.TaskId,
                });
            }
            
            isTrue = trueAnswer.Id == task.Answer;

            var progress = new UserProgress();
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == User.Identity.Name);
            progress.UserId = user.Id;
            progress.CourceId = (int)task.Id;
            progress.TaskId = (int)task.TaskId;
            progress.Point = isTrue? (double)task.Point : 0;

            _context.UserProgresses.Add(progress);
            _context.SaveChanges();

            return RedirectToAction("Study", "Cources", new { id = progress.CourceId, menuId = task.MenuId, submenuId = task.SubMenuId, taskId = task.TaskId, isTrue = isTrue, trueAnswer = trueAnswer.Option1, myAnswer = task.Answer});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Confirm(TaskViewModel model)
        {
            if (model == null)
            {
                return NotFound();
            }

            var trueOptions = _context.Options.Where(o => o.TaskId == model.TaskId && o.IsTrue);
            if (trueOptions.Any())
            {
                //task.Confirm = true
                ViewData["Confirm"] = true;
            }
            else 
            {
                ViewData["Confirm"] = false;
            }

            return RedirectToAction("Fill", "Cources", new { id = model.CourceId });

            /*var task = await model.ToTask(_environment);


            var courceId = TaskAction.Add(task, _context);

            if (courceId != -1)
            {
                return RedirectToAction("Fill", "Cources", new { id = courceId });
            }*/

            return Content("Record wasn't added");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(TaskViewModel model)
        {
            if (model == null)
            {
                return NotFound();
            }

            var task = await model.ToTask(_environment);
            
            var courceId = TaskAction.Add(task, _context);

            if (courceId != -1)
            {
                return RedirectToAction("Fill", "Cources", new { id = courceId, taskId = task.Id});
            }

            return Content("Record wasn't added");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(TaskViewModel model)
        {
            if (model == null)
            {
                return NotFound();
            }

            var task = await model.ToTask(_environment);
            var courceId = TaskAction.Delete(task.Id, _context);

            if (courceId != -1)
            {
                return RedirectToAction("Fill", "Cources", new { id = courceId });
            }

            return Content("Record wasn't deleted");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(TaskViewModel model)
        {
            if (model == null)
            {
                return NotFound();
            }

            var task = await model.ToTask(_environment);

            var courceId = TaskAction.Update(task, _context);

            if (courceId != -1)
            {
                return RedirectToAction("Fill", "Cources", new { id = courceId, taskId = task.Id });
            }

            return Content("Record wasn't edited");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOption(OptionViewModel model)
        {
            if (model == null)
            {
                return NotFound();
            }

            var option = model.ToOption();

            /*_context.Options.Add(option);
            _context.SaveChanges();*/

            var courceId = OptionAction.Add(option, _context);

            if (courceId != -1)
            {
            return RedirectToAction("Fill", "Cources", new { id = courceId, taskId = option.TaskId });
            }

            return Content("Record wasn't added");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteOption(OptionViewModel model)
        {
            if (model == null)
            {
                return NotFound();
            }

            var option = model.ToOption();
            var taskId = model.TaskId;

            var courceId = OptionAction.Delete(option.Id, _context);

            if (courceId != -1)
            {
                return RedirectToAction("Fill", "Cources", new { id = courceId, taskId = taskId });
            }

            return Content("Record wasn't deleted");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateOption(OptionViewModel model)
        {
            if (model == null)
            {
                return NotFound();
            }

            var option = model.ToOption();
            var taskId = option.TaskId;



            OptionAction.Delete(option.Id, _context);
            var courceId = OptionAction.Add(option, _context);

            if (courceId != -1)
            {
                return RedirectToAction("Fill", "Cources", new { id = courceId, taskId = taskId });
            }

            return Content("Record wasn't updated");
        }
    }
}
