using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using petsk.Models;


namespace petsk.Pages.Coll
{
    public class DeleteModel : PageModel
    {
        private readonly petsk.Models.PettContext _context;

        public DeleteModel(petsk.Models.PettContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Collectings == null)
            {
                return NotFound();
            }
            var collecting = await _context.Collectings.FindAsync(id);

            if (collecting != null)
            {
                Collecting = collecting;
                _context.Collectings.Remove(Collecting);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
