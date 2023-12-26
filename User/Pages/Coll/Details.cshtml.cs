using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using petsiki.Models;

namespace petsiki.Pages.Coll
{
    public class DetailsModel : PageModel
    {
        private readonly petsiki.Models.PettContext _context;

        public DetailsModel(petsiki.Models.PettContext context)
        {
            _context = context;
        }

        public Collecting Collecting { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Collectings == null)
            {
                return NotFound();
            }

            var collecting = await _context.Collectings.FirstOrDefaultAsync(m => m.IdCollecting == id);
            if (collecting == null)
            {
                return NotFound();
            }
            else
            {
                Collecting = collecting;
            }
            return Page();
        }
    }
}
