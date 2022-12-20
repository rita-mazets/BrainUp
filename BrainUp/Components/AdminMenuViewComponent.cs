using BrainUp.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;

namespace BrainUp.Components
{
    public class AdminMenuViewComponent: ViewComponent
    {
        private List<MenuItem> _menuItems = new List<MenuItem>
        {
            new MenuItem{ IsPage=true, Area="Admin", Page="Users",Text="Users"},
            new MenuItem{ IsPage=true, Area="Admin", Page="Categories/Index",Text="Categories"},
            new MenuItem{ IsPage=true, Area="Admin", Page="Currencies/Index",Text="Currencies"},
            new MenuItem{ IsPage=true, Area="Admin", Page="Languages/Index",Text="Languages"},
            new MenuItem{ IsPage=true, Area="Admin", Page="Levels/Index",Text="Levels"},

        };

        public IViewComponentResult Invoke()
        {
            var controller = ViewContext.RouteData.Values["controller"];
            var page = ViewContext.RouteData.Values["page"];
            var area = ViewContext.RouteData.Values["area"];
            foreach (var item in _menuItems)
            {
                var _matchController = controller?.Equals(item.Controller) ?? false;

                var _matchArea = area?.Equals(item.Area) ?? false;

                if (_matchController || _matchArea)
                {
                    item.Active = "active";
                }
            }
            return View(_menuItems);
        }
    }
}
