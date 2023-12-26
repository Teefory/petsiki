using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using petsiki.Models;

namespace petsiki.Pages.Coll
{
    public class EditModel : PageModel
    {
        private readonly petsiki.Models.PettContext _context;

        public EditModel(petsiki.Models.PettContext context)
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
            Collecting = collecting;
            ViewData["IdPet"] = new SelectList(_context.Pets, "IdPet", "IdPet");
            ViewData["IdShelter"] = new SelectList(_context.Shelters, "IdShelter", "IdShelter");
            ViewData["IdUser"] = new SelectList(_context.Users, "IdUser", "IdUser");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                _context.Attach(Collecting).State = EntityState.Modified;
                //return Page();
            }

            _context.Attach(Collecting).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CollectingExists(Collecting.IdCollecting))
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

        private bool CollectingExists(int id)
        {
            return (_context.Collectings?.Any(e => e.IdCollecting == id)).GetValueOrDefault();
        }
    }
}
