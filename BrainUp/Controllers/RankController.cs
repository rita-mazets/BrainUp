using BrainUp.Data;
using BrainUp.Models;
using BrainUp.StoredProcedure;
using BrainUp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BrainUp.Controllers
{
    public class RankController : Controller
    {
        private readonly BrainUpBdContext _context;

        public RankController(BrainUpBdContext context, IWebHostEnvironment environment)
        {
            _context = context;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(RankViewModel model)
        {
            if (model == null)
            {
                return NotFound();
            }

            var rank = model.ToRank();

            _context.Ranks.Add(rank);
            _context.SaveChanges();

            return RedirectToAction("Details", "Cources", new { id = rank.CourceId });


        }
    }
}
