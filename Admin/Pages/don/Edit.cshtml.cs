using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using petsk.Models;
using petsk.Pages.don;

namespace petsk.Pages.don
{
    public class EditModel : Donclass
    {
        private readonly petsk.Models.PettContext _context;

        public EditModel(petsk.Models.PettContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Donation Donation { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null /*|| _context.Donations == null*/)
            {
                return NotFound();
            }


            Donation = await _context.Donations
                             .Include(c => c.IdCollectingNavigation).FirstOrDefaultAsync(m => m.IdDonation == id);
            Donation = await _context.Donations
                             .Include(c => c.IdUserNavigation).FirstOrDefaultAsync(m => m.IdDonation == id);


            //.FirstOrDefaultAsync(m => m.IdDonation == id);
            if (Donation == null)
            {
                return NotFound();
            }


            PopulateUserDropDownList(_context,
                                     Donation.IdUser);
            PopulateCollDropDownList(_context,
                                     Donation.IdCollecting);
            //Donation = donation;
            //ViewData["IdCollecting"] = new SelectList(_context.Collectings, "IdCollecting", "IdCollecting");
            //ViewData["IdUser"] = new SelectList(_context.Users, "IdUser", "IdUser");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var courseToUpdate = await _context.Donations.FindAsync(id);

            if (courseToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Donation>(
                 courseToUpdate,
                 "Donation",   // Prefix for form value.
                   c => c.Data, c => c.IdUser, c => c.Amount, c => c.IdCollecting))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");

        }

        private bool donationexists(int id)
        {
            return (_context.Donations?.Any(e => e.IdDonation == id)).GetValueOrDefault();
        }
    }
}
