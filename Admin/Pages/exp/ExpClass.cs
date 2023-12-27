using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using petsk.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using petsk.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace petsk.Pages.exp
{
    public class ExpClass: PageModel
    {

        public SelectList CollectingNameSL { get; set; }
        public void PopulateCollDropDownList(PettContext _context,
            object selectedCollecting = null)
        {
            var CollectingQuery = from n in _context.Collectings
                                  orderby n.DescriptionC
                                  select n;

            CollectingNameSL = new SelectList(CollectingQuery.AsNoTracking(),
                nameof(Collecting.IdCollecting),
                nameof(Collecting.DescriptionC),
                selectedCollecting);
        }

    }

}

