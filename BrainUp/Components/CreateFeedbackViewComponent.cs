using BrainUp.Data;
using BrainUp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BrainUp.Components
{
    public class CreateFeedbackViewComponent: ViewComponent
    {
        private readonly BrainUpBdContext _context;

        public CreateFeedbackViewComponent(BrainUpBdContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(int id)
        {
            var feedback = new Feedback();
            feedback.CourceId = id;

            return View(feedback);
        }
    }
}
