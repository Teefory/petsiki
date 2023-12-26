using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using petsiki.Models;


namespace petsiki.Pages.Coll
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
            ViewData["IdPet"] = new SelectList(_context.Pets, "IdPet", "IdPet");
            ViewData["IdShelter"] = new SelectList(_context.Shelters, "IdShelter", "IdShelter");
            ViewData["IdUser"] = new SelectList(_context.Users, "IdUser", "IdUser");
            return Page();
        }

        [BindProperty]
        public Collecting Collecting { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Collectings == null || Collecting == null)
            {
                _context.Collectings.Add(Collecting);
                await _context.SaveChangesAsync();
                //return Page();
            }



            return RedirectToPage("./Index");
        }
    }
}
