using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using petsk.Models;

namespace petsk.Pages.recom
{
    public class DetailsModel : PageModel
    {
        private readonly petsk.Models.PettContext _context;

        public DetailsModel(petsk.Models.PettContext context)
        {
            _context = context;
        }

      public RecordingWalk RecordingWalk { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.RecordingWalks == null)
            {
                return NotFound();
            }

            var recordingwalk = await _context.RecordingWalks.FirstOrDefaultAsync(m => m.IdPet == id);
            if (recordingwalk == null)
            {
                return NotFound();
            }
            else 
            {
                RecordingWalk = recordingwalk;
            }
            return Page();
        }
    }
}
