using BrainUp.Data;
using BrainUp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BrainUp.Components
{
    public class FilterViewComponent: ViewComponent
    {
        private readonly BrainUpBdContext _context;

        public FilterViewComponent(BrainUpBdContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            ViewData["CategoryName"] = new SelectList(_context.Categories, "Id", "Name");

            return View();
        }
    }
}
