using BrainUp.Data;
using BrainUp.Models;
using BrainUp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BrainUp.Components.SubmenuActions
{
    public class CreateContentViewComponent : ViewComponent
    {
        private readonly BrainUpBdContext _context;

        public CreateContentViewComponent(BrainUpBdContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(int id)
        {
            var content = new Content();

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

            ViewData["SubMenu"] = new SelectList(dict, "Key", "Value");

            return View(content);
        }
    }
}
