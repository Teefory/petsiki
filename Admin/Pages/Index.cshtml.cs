using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using petsk.Models;

namespace petsk.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly petsk.Models.PettContext _context;

        public IList<Collecting> Collecting { get; set; } = default!;

        public IndexModel(ILogger<IndexModel> logger, petsk.Models.PettContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult OnGet() //IActionResult void
        {

            //if (_context.Collectings != null)
            //{

            //    Collecting = _context.Collectings.Where(
            //        c => new DateTime(c.ClosingDate.Year, c.ClosingDate.Month, c.ClosingDate.Day) >= DateTime.Today &&
            //        c.AlreadyAssembled < c.RequiredAmount
            //    ).ToList();
                
            //}
            return Redirect("/coll/Index");
        }
    }
}
