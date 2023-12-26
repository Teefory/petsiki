using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using petsiki.Models;

namespace petsiki.Pages.exp
{
    public class DetailsModel : PageModel
    {
        private readonly petsiki.Models.PettContext _context;

        public DetailsModel(petsiki.Models.PettContext context)
        {
            _context = context;
        }

        public Expense Expense { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Expenses == null)
            {
                return NotFound();
            }

            var expense = await _context.Expenses.FirstOrDefaultAsync(m => m.IdExpenses == id);
            if (expense == null)
            {
                return NotFound();
            }
            else
            {
                Expense = expense;
            }
            return Page();
        }
    }
}
