using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BrainUp.Data;
using BrainUp.Models;
using Task = System.Threading.Tasks.Task;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace BrainUp.Areas.Admin.Pages.Languages
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly BrainUp.Data.BrainUpBdContext _context;

        public IndexModel(BrainUp.Data.BrainUpBdContext context)
        {
            _context = context;
        }

        public IList<Language> Language { get;set; } = default!;

        public async System.Threading.Tasks.Task OnGetAsync()
        {
            if (_context.Languages != null)
            {
                Language = await _context.Languages.ToListAsync();
            }
        }
    }
}
