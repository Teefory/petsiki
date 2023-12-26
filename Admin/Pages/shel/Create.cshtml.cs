using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

using petsk.Models;

namespace petsk.Pages.shel
{
    public class CreateModel : PageModel
    {
        private readonly petsk.Models.PettContext _context;

        public CreateModel(petsk.Models.PettContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Shelter Shelter { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Shelters == null || Shelter == null)
            {
                return Page();
            }

            _context.Shelters.Add(Shelter);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
