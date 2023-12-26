using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using petsiki.Models;

namespace petsiki.Pages.shel
{
    public class IndexModel : PageModel
    {
        private readonly petsiki.Models.PettContext _context;

        public IndexModel(petsiki.Models.PettContext context)
        {
            _context = context;
        }

        public IList<Shelter> Shelter { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Shelters != null)
            {
                Shelter = await _context.Shelters.ToListAsync();
            }
        }
    }
}
