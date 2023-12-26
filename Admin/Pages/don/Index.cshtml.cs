using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using petsk.Models;

namespace petsk.Pages.don
{
    public class IndexModel : PageModel
    {
        private readonly petsk.Models.PettContext _context;

        public IndexModel(petsk.Models.PettContext context)
        {
            _context = context;
        }

        public IList<Donation> Donation { get; set; } = default!;

        public async Task OnGetAsync()
        {

            if (_context.Donations != null)
            {
                //Donation = Donation.OrderByDescending(date => date.Data).ToList();
                Donation = await _context.Donations
                .Include(d => d.IdCollectingNavigation)
                .Include(d => d.IdUserNavigation).OrderByDescending(date => date.Data).ToListAsync();
                //Donation = Donation.OrderByDescending(date => date.Data).ToList(); Take(10)


            }
        }
    }
}
