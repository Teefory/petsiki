using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using petsk.Models;

namespace petsk.Pages.recom
{
    public class CreateModel : RecomClass
    {
        private readonly petsk.Models.PettContext _context;

        public CreateModel(petsk.Models.PettContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            PopulateUsersDropDownList(_context);
            PopulatePetDropDownList(_context);
            //    ViewData["IdPet"] = new SelectList(_context.Pets, "IdPet", "IdPet");
            //ViewData["IdUser"] = new SelectList(_context.Users, "IdUser", "IdUser");
            return Page();
        }

        [BindProperty]
        public RecordingWalk RecordingWalk { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var emptyDon = RecordingWalk;


            var recordingWalks = _context.RecordingWalks.ToList();

            bool isOverlap = false;
            foreach (var record in recordingWalks)
            {
                if (emptyDon.IdPet != record.IdPet) continue;
                if(emptyDon.DataR != record.DataR) continue;

                bool isRecordSmaller = emptyDon.BeginWalk < record.BeginWalk;

                if (isRecordSmaller)
                {
                    isOverlap = emptyDon.BeginWalk <= record.BeginWalk && record.EndWalk <= emptyDon.EndWalk;
                    if(isOverlap) break;
                }
                isOverlap = record.BeginWalk <= emptyDon.BeginWalk && emptyDon.EndWalk <= record.EndWalk;
                if(isOverlap) break;
            }

            if (isOverlap)
            {
                // Добавляем кастомное сообщение об ошибке
                ModelState.AddModelError("", "Питомец в это время уже на прогулке");
                PopulateUsersDropDownList(_context);
                PopulatePetDropDownList(_context);
                return Page();
            }


            if (await TryUpdateModelAsync<RecordingWalk>(
                 emptyDon,
                 "recordingwalk",   // Prefix for form value.
                 s => s.IdUser, s => s.IdPet, s => s.DataR, s => s.BeginWalk, s => s.EndWalk))
            {

                _context.RecordingWalks.Add(emptyDon);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            _context.RecordingWalks.Add(emptyDon);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");

        }
    }
}
