using BrainUp.Data;
using BrainUp.Models;
using BrainUp.StoredProcedure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Stripe;
using System.Drawing;

namespace BrainUp.Components.TaskActions
{
    public class DeleteTaskViewComponent: ViewComponent
    {
        private readonly BrainUpBdContext _context;

        public DeleteTaskViewComponent(BrainUpBdContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(int id)
        {
            var content = new Content();

            var list = ContentAction.ReadAllMenuWithSubmenuContentForCource(id, _context);

            Dictionary<int, string> dict = new();

            if (list != null)
            {
                foreach (var item in list)
                {
                    int itemId = item.ContentId;
                    dict[itemId] = item.MenuName + " | " + item.SubmenuName + " | " + item.ContentName;
                }
            }

            ViewData["ContentMenu"] = new SelectList(dict, "Key", "Value");

            return View(content);
        }
    }
}
