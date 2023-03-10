using BrainUp.Data;
using BrainUp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BrainUp.Components
{
    public class StudyNavigationMenuViewComponent:ViewComponent
    {
        private readonly BrainUpBdContext _context;

        public StudyNavigationMenuViewComponent(BrainUpBdContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke(int? id)
        {
            var menus = _context.Menus.Where(c => c.CourceId == id).ToList();

            foreach (var menu in menus)
            {
                var submenu = _context.SubMenus.Include(s => s.Tasks).Include(s => s.Contents).Where(c => c.MenuId == menu.Id).ToList();

            }

            return View(menus as IEnumerable<Menu>);
        }
    }
}
