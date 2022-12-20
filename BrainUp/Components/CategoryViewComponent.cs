using BrainUp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BrainUp.Components
{
    public class CategoryViewComponent: ViewComponent
    {
        private readonly BrainUpBdContext _context;

        public CategoryViewComponent(BrainUpBdContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(int id)
        {

            ViewData["CategoryName"] = new SelectList(_context.Categories, "Id", "Name");


            return View();
        }
    }
}
