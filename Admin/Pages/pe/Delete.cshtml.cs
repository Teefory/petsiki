using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using petsk.Models;

namespace petsk.Pages.pe
{
    public class DeleteModel : PeClass
    {
        private readonly petsk.Models.PettContext _context;

        public DeleteModel(petsk.Models.PettContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Pet Pet { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Pets == null)
            {
                return NotFound();
            }

            Pet = await _context.Pets
           .AsNoTracking()
           .Include(c => c.IdShelterNavigation)
           .FirstOrDefaultAsync(m => m.IdPet== id);

            if (Pet == null)
            {
                return NotFound();
            }
            else
            {
                Pet = Pet;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Pets == null)
            {
                return NotFound();
            }
            var pet = await _context.Pets.FindAsync(id);

            if (pet != null)
            {
                Pet = pet;
                _context.Pets.Remove(Pet);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/recom/Index");
        }
    }
}
