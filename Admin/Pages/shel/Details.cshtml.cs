using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using petsk.Models;

namespace petsk.Pages.shel
{
    public class DetailsModel : PageModel
    {
        private readonly petsk.Models.PettContext _context;

        public DetailsModel(petsk.Models.PettContext context)
        {
            _context = context;
        }

        public Shelter Shelter { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Shelters == null)
            {
                return NotFound();
            }

            var shelter = await _context.Shelters.FirstOrDefaultAsync(m => m.IdShelter == id);
            if (shelter == null)
            {
                return NotFound();
            }
            else
            {
                Shelter = shelter;
            }
            return Page();
        }
    }
}
