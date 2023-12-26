using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using petsiki.Models;

namespace petsiki.Pages.recom
{
    public class EditModel : PageModel
    {
        private readonly petsiki.Models.PettContext _context;

        public EditModel(petsiki.Models.PettContext context)
        {
            _context = context;
        }

        [BindProperty]
        public RecordingWalk RecordingWalk { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(DateOnly? id)
        {
            if (id == null || _context.RecordingWalks == null)
            {
                return NotFound();
            }

            var recordingwalk =  await _context.RecordingWalks.FirstOrDefaultAsync(m => m.DataR == id);
            if (recordingwalk == null)
            {
                return NotFound();
            }
            RecordingWalk = recordingwalk;
           ViewData["IdPet"] = new SelectList(_context.Pets, "IdPet", "IdPet");
           ViewData["IdUser"] = new SelectList(_context.Users, "IdUser", "IdUser");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(RecordingWalk).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecordingWalkExists(RecordingWalk.DataR))
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

        private bool RecordingWalkExists(DateOnly id)
        {
          return (_context.RecordingWalks?.Any(e => e.DataR == id)).GetValueOrDefault();
        }
    }
}
