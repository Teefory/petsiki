using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using petsk.Models;

namespace petsk.Pages.don
{
    public class CreateModel : Donclass
    {
        private readonly petsk.Models.PettContext _context;

        public CreateModel(petsk.Models.PettContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            PopulateUserDropDownList(_context);
            PopulateCollDropDownList(_context);
            //ViewData["IdCollecting"] = new SelectList(_context.Collectings, "IdCollecting", "IdCollecting");
            //ViewData["IdUser"] = new SelectList(_context.Users, "IdUser", "IdUser");
            return Page();
        }

        [BindProperty]
        public Donation Donation { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {

            //if (!ModelState.IsValid || _context.Donations == null || Donation == null) //это моё
            //{
            //    return Page();
            //}
            //_context.Donations.Add(Donation);
            //await _context.SaveChangesAsync();
            //return RedirectToPage("./Index");


            var emptyDon = new Donation();

            //Collecting = await _context.Collectings.Where(
            //      c => new DateTime(c.ClosingDate.Year, c.ClosingDate.Month, c.ClosingDate.Day) >= DateTime.Today &&
            //      c.AlreadyAssembled < c.RequiredAmount


            //      ).ToListAsync();

            if (await TryUpdateModelAsync<Donation>(
                 emptyDon,
                 "donation",   // Prefix for form value.
                 s => s.IdDonation, s => s.Data, s => s.Amount, s => s.IdUser, s => s.IdCollecting))
            //s => s.IdDonation, s => s.Data, s => s.Amount, s => s.IdUser,  s => s.IdCollecting))
            {

                _context.Donations.Add(emptyDon);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            _context.Donations.Add(emptyDon);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");

            // Select DepartmentID if TryUpdateModelAsync fails.

            PopulateUserDropDownList(_context, emptyDon.IdUserNavigation);
            PopulateCollDropDownList(_context, emptyDon.IdCollectingNavigation);
            return Page();



        }
    }
}
