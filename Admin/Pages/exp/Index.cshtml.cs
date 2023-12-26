using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using petsk.Models;

namespace petsk.Pages.exp
{
    public class IndexModel : PageModel
    {
        private readonly petsk.Models.PettContext _context;

        public IndexModel(petsk.Models.PettContext context)
        {
            _context = context;
        }

        public IList<Expense> Expense { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Expenses != null)
            {
                Expense = await _context.Expenses
                .Include(e => e.IdCollectingNavigation).ToListAsync();
            }
        }
    }
}
