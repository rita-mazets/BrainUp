using BrainUp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BrainUp.Components.SubmenuActions
{
    public class CreateMenuViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(int id)
        {
            var menu = new Menu();
            menu.CourceId = id;
            return View(menu);
        }
    }
}
