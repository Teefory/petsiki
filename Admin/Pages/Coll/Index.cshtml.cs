using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using petsk.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;



namespace petsk.Pages.Coll
{

    public class IndexModel : PageModel
    {

        private readonly petsk.Models.PettContext _context;

        public IndexModel(petsk.Models.PettContext context)
        {
            _context = context;

        }

        public string a;

        public IList<Collecting> Collecting { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Collectings != null)
            {
                Collecting = await _context.Collectings
                .Include(c => c.IdPetNavigation)
                .Include(c => c.IdShelterNavigation)
                .Include(c => c.IdUserNavigation).ToListAsync();

                //Collecting = await _context.Collectings.Where(
                //    c => new DateTime(c.ClosingDate.Year, c.ClosingDate.Month, c.ClosingDate.Day) >= DateTime.Today &&
                //    c.AlreadyAssembled < c.RequiredAmount


                //).ToListAsync();
            }
        }
        public DateTime DateTime1 { get; set; }



        // is same as  

        DateTime DateTime2 = DateTime.Now;
    }

}
