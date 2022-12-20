using BrainUp.Data;
using BrainUp.Models;
using BrainUp.StoredProcedure;
using BrainUp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BrainUp.Controllers
{
    public class ContentController : Controller
    {
        private readonly BrainUpBdContext _context;

        public ContentController(BrainUpBdContext context)
        {
            _context = context;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Content content)
        {
            if (content == null)
            {
                return NotFound();
            }

            var courceId = ContentAction.Add(content, _context);

            if (courceId != -1)
            {
                return RedirectToAction("Fill", "Cources", new { id = courceId });
            }

            return Content("Record wasn't added");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(ContentViewModel model)
        {
            if (model == null)
            {
                return NotFound();
            }

            Content content = model.ToContent();

            var courceId = CategoryAction.Delete(content.Id, _context);

            if (courceId != -1)
            {
                return RedirectToAction("Fill", "Cources", new { id = courceId });
            }

            return Content("Record wasn't deleted");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(ContentViewModel model)
        {
              if (model == null)
              {
                  return NotFound();
              }

            Content content = model.ToContent();


            var courceId = ContentAction.Update(content, _context);

            if (courceId != -1)
            {
                return RedirectToAction("Fill", "Cources", new { id = courceId, contentId = content.Id });
            }

            return Content("Record wasn't edited");
        }
    }
}
