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
    public class IndexModel : PageModel
    {
        private readonly petsk.Models.PettContext _context;

        public IndexModel(petsk.Models.PettContext context)
        {
            _context = context;
        }

        public IList<RecordingWalk> RecordingWalk { get; set; } = default!;
        public IList<Pet> Pets { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.RecordingWalks != null)
            {
                RecordingWalk = await _context.RecordingWalks
                .Include(r => r.IdPetNavigation)
                .Include(r => r.IdUserNavigation).ToListAsync();
            }


            if (_context.Pets != null)
            {
                Pets = await _context.Pets
               .Include(p => p.IdShelterNavigation).ToListAsync();
            }
        }
    }
}
