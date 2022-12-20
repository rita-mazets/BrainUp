using BrainUp.Data;
using BrainUp.Models;
using BrainUp.StoredProcedure;
using BrainUp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BrainUp.Components.ContentActions
{
    public class ReadContentViewComponent: ViewComponent
    {
        private readonly BrainUpBdContext _context;

        public ReadContentViewComponent(BrainUpBdContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(int contentId)
        {
            var contents = CategoryAction.ReadContent(contentId, _context);

            Content content = null;

            if (contents != null && contents.Any())
            {
                content = contents.First();
            }

            var model = new ContentViewModel();
            model.FromContent(content);

            return View(model);
        }
    }
}
