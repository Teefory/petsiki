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
    public class DeleteModel : PageModel
    {
        private readonly petsk.Models.PettContext _context;

        public DeleteModel(petsk.Models.PettContext context)
        {
            _context = context;
        }

        [BindProperty]
        public RecordingWalk RecordingWalk { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.RecordingWalks == null) return NotFound();

            RecordingWalk = await _context.RecordingWalks.FirstOrDefaultAsync(m => m.IdRecordingWalk == id);

            if (RecordingWalk == null) return NotFound();
            return Page();
            
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.RecordingWalks == null) return NotFound();

            RecordingWalk = await _context.RecordingWalks.FindAsync(id);

            if (RecordingWalk != null)
            {
                _context.RecordingWalks.Remove(RecordingWalk);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
