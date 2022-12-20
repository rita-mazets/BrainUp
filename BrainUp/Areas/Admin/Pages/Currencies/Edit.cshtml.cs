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

namespace BrainUp.Areas.Admin.Pages.Currencies
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
        public Currency Currency { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null || _context.Currencies == null)
            {
                return NotFound();
            }

            var currency =  await _context.Currencies.FirstOrDefaultAsync(m => m.Symbol == id);
            if (currency == null)
            {
                return NotFound();
            }
            Currency = currency;
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

            _context.Attach(Currency).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CurrencyExists(Currency.Symbol))
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

        private bool CurrencyExists(string id)
        {
          return _context.Currencies.Any(e => e.Symbol == id);
        }
    }
}
