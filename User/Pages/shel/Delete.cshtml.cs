using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using petsiki.Models;

namespace petsiki.Pages.shel
{
    public class DeleteModel : PageModel
    {
        private readonly petsiki.Models.PettContext _context;

        public DeleteModel(petsiki.Models.PettContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Shelter Shelter { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Shelters == null)
            {
                return NotFound();
            }

            var shelter = await _context.Shelters.FirstOrDefaultAsync(m => m.IdShelter == id);

            if (shelter == null)
            {
                return NotFound();
            }
            else
            {
                Shelter = shelter;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Shelters == null)
            {
                return NotFound();
            }
            var shelter = await _context.Shelters.FindAsync(id);

            if (shelter != null)
            {
                Shelter = shelter;
                _context.Shelters.Remove(Shelter);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
