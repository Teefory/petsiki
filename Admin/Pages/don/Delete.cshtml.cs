﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using petsk.Models;

namespace petsk.Pages.don
{
    public class DeleteModel : PageModel
    {
        private readonly petsk.Models.PettContext _context;

        public DeleteModel(petsk.Models.PettContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Donation Donation { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Donations == null)
            {
                return NotFound();
            }

            var donation = await _context.Donations.FirstOrDefaultAsync(m => m.IdDonation == id);

            if (donation == null)
            {
                return NotFound();
            }
            else
            {
                Donation = donation;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Donations == null)
            {
                return NotFound();
            }
            var donation = await _context.Donations.FindAsync(id);

            if (donation != null)
            {
                Donation = donation;
                _context.Donations.Remove(Donation);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
