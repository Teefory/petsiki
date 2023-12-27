using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using petsk.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace petsk.Pages.pe
{
    public class PeClass:PageModel
    {
        public SelectList ShelterNameSL { get; set; }
        public void PopulateShelterDropDownList(PettContext _context,
            object selectedShelter = null)
        {
            var ShelterQuery = from n in _context.Shelters
                                  orderby n.Address
                                  select n;

            ShelterNameSL = new SelectList(ShelterQuery.AsNoTracking(),
                nameof(Shelter.IdShelter),
                nameof(Shelter.Address),
                selectedShelter);
        }


    }
}
