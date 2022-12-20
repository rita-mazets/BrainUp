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

namespace BrainUp.Areas.Admin.Pages.Currencies
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
      public Currency Currency { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null || _context.Currencies == null)
            {
                return NotFound();
            }

            var currency = await _context.Currencies.FirstOrDefaultAsync(m => m.Symbol == id);

            if (currency == null)
            {
                return NotFound();
            }
            else 
            {
                Currency = currency;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null || _context.Currencies == null)
            {
                return NotFound();
            }
            var currency = await _context.Currencies.FindAsync(id);

            if (currency != null)
            {
                Currency = currency;
                _context.Currencies.Remove(Currency);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
