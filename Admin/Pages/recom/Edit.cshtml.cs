using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using petsk.Models;

namespace petsk.Pages.recom
{
    public class EditModel : RecomClass
    {
        private readonly petsk.Models.PettContext _context;

        public EditModel(petsk.Models.PettContext context)
        {
            _context = context;
        }

        [BindProperty]
        public RecordingWalk RecordingWalk { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.RecordingWalks == null)
            {
                return NotFound();
            }

           
            RecordingWalk = await _context.RecordingWalks
                         .Include(c => c.IdUserNavigation).FirstOrDefaultAsync(m => m.IdRecordingWalk == id);
            RecordingWalk = await _context.RecordingWalks
                         .Include(c => c.IdPetNavigation).FirstOrDefaultAsync(m => m.IdRecordingWalk == id);


            //var recordingwalk = await _context.RecordingWalks.FirstOrDefaultAsync(m => m.IdRecordingWalk == id);
            if (RecordingWalk == null)
            {
                return NotFound();
            }



            PopulateUsersDropDownList(_context,
                                    RecordingWalk.IdUser);
            PopulatePetDropDownList(_context,
                                    RecordingWalk.IdPet);

            // RecordingWalk = recordingwalk;
            //ViewData["IdPet"] = new SelectList(_context.Pets, "IdPet", "IdPet");
            //ViewData["IdUser"] = new SelectList(_context.Users, "IdUser", "IdUser");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            //if (!ModelState.IsValid)

            //{
            //    _context.Attach(RecordingWalk).State = EntityState.Modified;
            //    //return Page();
            //}

            //_context.Attach(RecordingWalk).State = EntityState.Modified;

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!RecordingWalkExists(RecordingWalk.IdPet))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            //return RedirectToPage("./Index");

            if (id == null)
            {
                return NotFound();
            }

            var courseToUpdate = await _context.RecordingWalks.FindAsync(id);

            if (courseToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<RecordingWalk>(
                 courseToUpdate,
                 "RecordingWalk",   // Prefix for form value.
                 s => s.IdPet, s => s.DataR, s => s.IdUser, s => s.BeginWalk, s => s.EndWalk))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("/recom/Index");
            }
            await _context.SaveChangesAsync();
            return RedirectToPage("/recom/Index");
        }

        private bool RecordingWalkExists(int id)
        {
          return (_context.RecordingWalks?.Any(e => e.IdRecordingWalk == id)).GetValueOrDefault();
        }
    }
}
