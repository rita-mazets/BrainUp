using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BrainUp.Data;
using BrainUp.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace BrainUp.Areas.Admin.Pages.Currencies
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly BrainUp.Data.BrainUpBdContext _context;

        public CreateModel(BrainUp.Data.BrainUpBdContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Currency Currency { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Currencies.Add(Currency);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
