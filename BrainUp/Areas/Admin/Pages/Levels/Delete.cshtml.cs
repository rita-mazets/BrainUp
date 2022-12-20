using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BrainUp.Data;
using BrainUp.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace BrainUp.Areas.Admin.Pages.Levels
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly BrainUp.Data.BrainUpBdContext _context;

        public DeleteModel(BrainUp.Data.BrainUpBdContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Level Level { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Levels == null)
            {
                return NotFound();
            }

            var level = await _context.Levels.FirstOrDefaultAsync(m => m.Id == id);

            if (level == null)
            {
                return NotFound();
            }
            else 
            {
                Level = level;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Levels == null)
            {
                return NotFound();
            }
            var level = await _context.Levels.FindAsync(id);

            if (level != null)
            {
                Level = level;
                _context.Levels.Remove(Level);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
