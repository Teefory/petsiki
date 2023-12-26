using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using petsiki.Models;

namespace petsiki.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly petsiki.Models.PettContext _context;

        public IList<Collecting> Collecting { get; set; } = default!;

        public IndexModel(ILogger<IndexModel> logger, petsiki.Models.PettContext context)
        {
            _logger = logger; 
            _context = context;
        }

        public void OnGet() //IActionResult 
        {

            if (_context.Collectings != null)
            {

                Collecting = _context.Collectings.Where(
                    c => new DateTime(c.ClosingDate.Year, c.ClosingDate.Month, c.ClosingDate.Day) >= DateTime.Today &&
                    c.AlreadyAssembled < c.RequiredAmount
                ).ToList();
            }
            //return Redirect("/coll/Index");
        }
    }
}
