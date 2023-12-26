using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using petsiki.Models;


namespace petsiki.Pages.pe
{
    public class CreateModel : PageModel
    {
        private readonly petsiki.Models.PettContext _context;

        public CreateModel(petsiki.Models.PettContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["IdShelter"] = new SelectList(_context.Shelters, "IdShelter", "IdShelter");
            return Page();
        }

        [BindProperty]
        public Pet Pet { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Pets == null || Pet == null)
            {
                _context.Pets.Add(Pet);
                await _context.SaveChangesAsync();


            }


            // return Page();

            return RedirectToPage("./Index");

        }
    }
}
