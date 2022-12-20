using BrainUp.Data;
using BrainUp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BrainUp.Components.MenuActions
{
    public class EditMenuViewComponent:ViewComponent
    {
        private readonly BrainUpBdContext _context;

        public EditMenuViewComponent(BrainUpBdContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(int id)
        {
            var menu = new Menu();
            menu.CourceId = id;

            ViewData["AvailableMenu"] = new SelectList(_context.Menus.Where(m => m.CourceId == id), "Id", "Name");

            return View(menu);
        }
    }
}
