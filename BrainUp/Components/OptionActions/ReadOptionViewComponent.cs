using BrainUp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BrainUp.Components.OptionActions
{
    public class ReadOptionViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke(OptionViewModel model)
        {
            return View(model);
        }
    }
}
