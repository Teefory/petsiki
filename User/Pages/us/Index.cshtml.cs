using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using petsiki.Models;

namespace petsiki.Pages.us
{
    public class IndexModel : PageModel
    {
        private readonly petsiki.Models.PettContext _context;

        public IndexModel(petsiki.Models.PettContext context)
        {
            _context = context;
        }

        public IList<Users> User { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Users != null)
            {
                User = await _context.Users.ToListAsync();
            }
        }
    }
}
