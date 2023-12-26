using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

using petsiki.Models;

namespace petsiki.Pages.recom
{
    public class CreateModel : prog
    {
        private readonly petsiki.Models.PettContext _context;

        public CreateModel(petsiki.Models.PettContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            PopulateUsersDropDownList(_context);
            PopulatePetDropDownList(_context);
            //    ViewData["IdPet"] = new SelectList(_context.Pets, "IdPet", "IdPet");
            //ViewData["IdUser"] = new SelectList(_context.Users, "IdUser", "IdUser");
            return Page();
        }

        [BindProperty]
        public RecordingWalk RecordingWalk { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid || _context.RecordingWalks == null || RecordingWalk == null)
            //  {
            //      _context.RecordingWalks.Add(RecordingWalk);
            //      await _context.SaveChangesAsync();
            //      //return Page();
            //  }

            //  //_context.RecordingWalks.Add(RecordingWalk);
            //  //await _context.SaveChangesAsync();

            //  return RedirectToPage("./Index");

            var emptyDon = new RecordingWalk();


            if (await TryUpdateModelAsync<RecordingWalk>(
                 emptyDon,
                 "recordingwalk",   // Prefix for form value.
                 s => s.IdUser, s => s.IdPet, s => s.DataR, s => s.BeginWalk, s => s.EndWalk))
            //s => s.IdDonation, s => s.Data, s => s.Amount, s => s.IdUser,  s => s.IdCollecting))
            {

                _context.RecordingWalks.Add(emptyDon);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            _context.RecordingWalks.Add(emptyDon);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");

            // Select DepartmentID if TryUpdateModelAsync fails.

            PopulateUsersDropDownList(_context, emptyDon.IdUserNavigation);
            PopulatePetDropDownList(_context, emptyDon.IdPetNavigation);

            return Page();

        }
    }
}
