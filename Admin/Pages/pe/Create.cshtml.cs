using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using petsk.Models;


namespace petsk.Pages.pe
{
    public class CreateModel : PeClass
    {
        private readonly petsk.Models.PettContext _context;

        public CreateModel(petsk.Models.PettContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            PopulateShelterDropDownList(_context);
            //ViewData["IdShelter"] = new SelectList(_context.Shelters, "IdShelter", "IdShelter");
            return Page();
        }

        [BindProperty]
        public Pet Pet { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var emptyPet = new Pet();

            if (await TryUpdateModelAsync<Pet>(
                 emptyPet,
                 "pet",   // Prefix for form value.
                 s => s.IdPet, s => s.Nickname, s => s.DateOfBirth, s => s.DescriptionP, s => s.MedicalInformation, s => s.IdShelter))
            //s => s.IdDonation, s => s.Data, s => s.Amount, s => s.IdUser,  s => s.IdCollecting))
            {

                _context.Pets.Add(emptyPet); 
                await _context.SaveChangesAsync();
                return RedirectToPage("/recom/Index");
            }

            _context.Pets.Add(emptyPet);
            await _context.SaveChangesAsync();
            return RedirectToPage("/recom/Index");

        }
    }
}
