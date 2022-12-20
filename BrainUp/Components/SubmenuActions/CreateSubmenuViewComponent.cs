using BrainUp.Data;
using BrainUp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BrainUp.Components.SubmenuActions
{
    public class CreateSubMenuViewComponent:ViewComponent
    {
        private readonly BrainUpBdContext _context;

        public CreateSubMenuViewComponent(BrainUpBdContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(int id)
        {
            var submenu = new SubMenu();

            ViewData["AvailableMenu"] = new SelectList(_context.Menus.Where(m => m.CourceId == id), "Id", "Name");

            return View(submenu);
        }
    }
}
