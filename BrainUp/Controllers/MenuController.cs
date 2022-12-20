using BrainUp.Data;
using BrainUp.Models;
using BrainUp.StoredProcedure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BrainUp.Controllers
{
    public class MenuController : Controller
    {
        private readonly BrainUpBdContext _context;

        public MenuController(BrainUpBdContext context, IWebHostEnvironment environment)
        {
            _context = context;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Menu menu)
        {
            if (menu == null || menu.Name == null)
            {
                return NotFound();
            }

            var newId = MenuAction.Add(menu, _context);

            if (newId != -1)
            {
                return RedirectToAction("Fill", "Cources", new { id = menu.CourceId });
            }

            return Content("Record wasn't added");
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Menu menu)
        {
            if (menu == null)
            {
                return NotFound();
            }

            var result = MenuAction.Delete(menu.Id, _context);

            if (result == 1)
            {
                return RedirectToAction("Fill", "Cources", new { id = menu.CourceId });
            }

            return Content("Record wasn't deleted");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Menu menu)
        {
            if (menu == null)
            {
                return NotFound();
            }

            var result = MenuAction.Update(menu, _context);

            if (result == 1)
            {
                return RedirectToAction("Fill", "Cources", new { id = menu.CourceId });
            }

            return Content("Record wasn't edited");
        }
    }
}
