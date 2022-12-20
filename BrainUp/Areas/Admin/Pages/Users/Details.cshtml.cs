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

namespace BrainUp.Areas.Admin.Pages.Users
{
    [Authorize(Roles = "Admin")]
    public class DetailsModel : PageModel
    {
        private readonly BrainUp.Data.BrainUpBdContext _context;

        public DetailsModel(BrainUp.Data.BrainUpBdContext context)
        {
            _context = context;
        }

      public User User { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            else 
            {
                User = user;
            }
            return Page();
        }
    }
}
