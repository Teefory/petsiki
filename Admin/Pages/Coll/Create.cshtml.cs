using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using petsk.Models;


namespace petsk.Pages.Coll
{
    public class CreateModel : Collec
    {
        private readonly petsk.Models.PettContext _context;

        public CreateModel(petsk.Models.PettContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            PopulateUserDropDownList(_context);
            PopulateShelterDropDownList(_context);
            PopulatePetDropDownList(_context);

            //ViewData["IdPet"] = new SelectList(_context.Pets, "IdPet", "IdPet");
            //ViewData["IdShelter"] = new SelectList(_context.Shelters, "IdShelter", "IdShelter");
            //ViewData["IdUser"] = new SelectList(_context.Users, "IdUser", "IdUser");
            return Page();
        }

        [BindProperty]
        public Collecting Collecting { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid || _context.Collectings == null || Collecting == null)
            //{
            //    _context.Collectings.Add(Collecting);
            //    await _context.SaveChangesAsync();
            //    //return Page();
            //}

            //return RedirectToPage("./Index");

            var emptyColl = new Collecting();
                   
            if (await TryUpdateModelAsync<Collecting>(
                 emptyColl,
                 "collecting",   // Prefix for form value.
                 s => s.IdCollecting, s => s.DescriptionC, s => s.AlreadyAssembled, s => s.RequiredAmount, s => s.OpeningDate, s => s.ClosingDate, s => s.IdPet, s => s.IdShelter, s => s.IdUser))
            
            {

                _context.Collectings.Add(emptyColl);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            _context.Collectings.Add(emptyColl);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");

            // Select DepartmentID if TryUpdateModelAsync fails.

        }
    }
}
