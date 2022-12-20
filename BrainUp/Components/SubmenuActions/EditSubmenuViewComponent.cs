using BrainUp.Data;
using BrainUp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BrainUp.Components.SubmenuActions
{
    public class EditSubmenuViewComponent:ViewComponent
    {
        private readonly BrainUpBdContext _context;

        public EditSubmenuViewComponent(BrainUpBdContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(int id)
        {
            var sub = new SubMenu();

            var menus = _context.Menus.Include(m => m.SubMenus).Where(m => m.CourceId == id);
            Dictionary<int, string> dict = new();


            foreach (var menu in menus)
            {
                foreach (var item in menu.SubMenus)
                {
                    int itemId = item.Id;
                    dict[itemId] = menu.Name + " | " + item.Name;
                }
            }

            ViewData["Menu"] = new SelectList(dict, "Key", "Value");

            //ViewData["AvailableSubMenu"] = new SelectList(_context.SubMenus.Where(m => m.MenuId == menuId), "Id", "Name");

            return View(sub);
        }
    }
}
