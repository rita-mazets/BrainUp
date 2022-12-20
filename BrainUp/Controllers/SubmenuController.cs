using BrainUp.Data;
using BrainUp.Models;
using BrainUp.StoredProcedure;
using Microsoft.AspNetCore.Mvc;

namespace BrainUp.Controllers
{
    public class SubmenuController : Controller
    {
        private readonly BrainUpBdContext _context;

        public SubmenuController(BrainUpBdContext context)
        {
            _context = context;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(SubMenu submenu)
        {
            if (submenu == null || submenu.Name == null)
            {
                return NotFound();
            }

            (int newId, int courceid) = SubmenuAction.Add(submenu, _context);

            if (newId != -1)
            {
                return RedirectToAction("Fill", "Cources", new { id = courceid});
            }

            return Content("Submenu wasn't added");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(SubMenu submenu)
        {
            if (submenu == null)
            {
                return NotFound();
            }

            var result = _context.SubMenus.Remove(submenu);
            await _context.SaveChangesAsync();

            /*var result = SubmenuAction.Delete(submenu.Id, _context);

            if (result != -1)
            {*/
                return RedirectToAction("Fill", "Cources", new { id = 1005 });
            /*}

            return Content("Record wasn't deleted");*/
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(SubMenu submenu)
        {
            if (submenu == null)
            {
                return NotFound();
            }

            var courceId = SubmenuAction.Update(submenu, _context);

            if (courceId != -1)
            {
                return RedirectToAction("Fill", "Cources", new { id = courceId });
            }

            return Content("Record wasn't edited");
        }
    }
}
