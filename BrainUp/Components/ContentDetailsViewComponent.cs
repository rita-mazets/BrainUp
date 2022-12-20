using BrainUp.Data;
using BrainUp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BrainUp.Components
{
    public class ContentDetailsViewComponent:ViewComponent
    {
        private readonly BrainUpBdContext _context;

        public ContentDetailsViewComponent(BrainUpBdContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int contentId)
        {
            var content = await _context.Contents.FirstOrDefaultAsync(c => c.Id == contentId);

            return View(content);
        }
    }
}
