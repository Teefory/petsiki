using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using petsk.Models;

namespace petsk.Pages.Coll
{
    public class EditModel : Collclass
    {
        private readonly petsk.Models.PettContext _context;

        public EditModel(petsk.Models.PettContext context)
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

            Collecting = await _context.Collectings
                             .Include(c => c.IdPetNavigation).FirstOrDefaultAsync(m => m.IdCollecting == id);
            Collecting = await _context.Collectings
                             .Include(c => c.IdShelterNavigation).FirstOrDefaultAsync(m => m.IdCollecting == id);
            Collecting = await _context.Collectings
                             .Include(c => c.IdUserNavigation).FirstOrDefaultAsync(m => m.IdCollecting == id);

            if (collecting == null)
            {
                return NotFound();
            }
            //Collecting = collecting;
            //ViewData["IdPet"] = new SelectList(_context.Pets, "IdPet", "IdPet");
            //ViewData["IdShelter"] = new SelectList(_context.Shelters, "IdShelter", "IdShelter");
            //ViewData["IdUser"] = new SelectList(_context.Users, "IdUser", "IdUser");

            PopulatePetDropDownList(_context,
                                    Collecting.IdPet);
            PopulateShelterDropDownList(_context,
                                     Collecting.IdShelter);
            PopulateUserDropDownList(_context,
                                     Collecting.IdUser);


            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            //    if (!ModelState.IsValid)
            //    {
            //        _context.Attach(Collecting).State = EntityState.Modified;
            //        //return Page();
            //    }

            //    _context.Attach(Collecting).State = EntityState.Modified;

            //    try
            //    {
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!CollectingExists(Collecting.IdCollecting))
            //        {
            //            return NotFound();
            //        }
            //        else
            //        {
            //            throw;
            //        }
            //    }

            //    return RedirectToPage("./Index");
            //}

            //private bool CollectingExists(int id)
            //{
            //    return (_context.Collectings?.Any(e => e.IdCollecting == id)).GetValueOrDefault();
            //}
            if (id == null)
            {
                return NotFound();
            }

            var courseToUpdate = await _context.Collectings.FindAsync(id);

            if (courseToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Collecting>(
                 courseToUpdate,
                 "Collecting",   // Prefix for form value.
                 s => s.DescriptionC, s => s.AlreadyAssembled, s => s.RequiredAmount, s => s.OpeningDate, s => s.ClosingDate, s => s.IdPet, s => s.IdShelter, s => s.IdUser))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            await _context.SaveChangesAsync();
            //PopulateUserDropDownList(_context, courseToUpdate.IdUserNavigation);
            //PopulateUserDropDownList(_context, courseToUpdate.IdCollectingNavigation);
            return RedirectToPage("./Index");
            // Select DepartmentID if TryUpdateModelAsync fails.
  
        }

        private bool donationexists(int id)
        {
            return (_context.Collectings?.Any(e => e.IdCollecting == id)).GetValueOrDefault();
        }
    }
}
