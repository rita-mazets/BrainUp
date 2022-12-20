using BrainUp.Data;
using BrainUp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BrainUp.Components.MenuActions
{
    public class DeleteMenuViewComponent: ViewComponent
    {
        private readonly BrainUpBdContext _context;

        public DeleteMenuViewComponent(BrainUpBdContext context)
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
