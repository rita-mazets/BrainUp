using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BrainUp.Data;
using BrainUp.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace BrainUp.Areas.Admin.Pages.Levels
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly BrainUp.Data.BrainUpBdContext _context;

        public EditModel(BrainUp.Data.BrainUpBdContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Level Level { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Levels == null)
            {
                return NotFound();
            }

            var level =  await _context.Levels.FirstOrDefaultAsync(m => m.Id == id);
            if (level == null)
            {
                return NotFound();
            }
            Level = level;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Level).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LevelExists(Level.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool LevelExists(int id)
        {
          return _context.Levels.Any(e => e.Id == id);
        }
    }
}
