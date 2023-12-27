using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using petsk.Models;

namespace petsk.Pages.pe
{
    public class EditModel : PeClass
    {
        private readonly petsk.Models.PettContext _context;

        public EditModel(petsk.Models.PettContext context)
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
                             .Include(c => c.IdShelterNavigation).FirstOrDefaultAsync(m => m.IdPet == id);

            var pet = await _context.Pets.FirstOrDefaultAsync(m => m.IdPet == id);
            if (pet == null)
            {
                return NotFound();
            }
            //Pet = pet;
            //ViewData["IdShelter"] = new SelectList(_context.Shelters, "IdShelter", "IdShelter");
            PopulateShelterDropDownList(_context,
                                     Pet.IdShelter);
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            //if (!ModelState.IsValid)
            //{
            //    _context.Attach(Pet).State = EntityState.Modified;
            //    //return Page();
            //}

            //_context.Attach(Pet).State = EntityState.Modified;

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!PetExists(Pet.IdPet))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            //return RedirectToPage("/recom/Index");
            if (id == null)
            {
                return NotFound();
            }

            var courseToUpdate = await _context.Pets.FindAsync(id);

            if (courseToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Pet>(
                 courseToUpdate,
                 "Pet",   // Prefix for form value.
                 s => s.Nickname, s => s.DateOfBirth, s => s.DescriptionP, s => s.MedicalInformation, s => s.IdShelter))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("/recom/Index");
            }
            await _context.SaveChangesAsync();
            return RedirectToPage("/recom/Index");
 
        }

        private bool PetExists(int id)
        {
            return (_context.Pets?.Any(e => e.IdPet == id)).GetValueOrDefault();
        }
    }
}
