//using petsik.Data;

using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using petsiki.Models;


namespace petsiki.Pages.don

{
    public class idname : PageModel
    {

        public SelectList UserNameSL { get; set; }

        public void PopulateUserDropDownList(PettContext _context,
            object selectedUser = null)
        {
            var UsersQuery = from m in _context.Users
                             orderby m.Name // Sort by name.
                             select m;

            UserNameSL = new SelectList(UsersQuery.AsNoTracking(),
                nameof(Users.IdUser),
                nameof(Users.Name),
                selectedUser);
        }
        public SelectList CollectingNameSL { get; set; }
        public void PopulateCollDropDownList(PettContext _context,
            object selectedCollecting = null)
        {
            var CollectingQuery = from n in _context.Collectings
                                  orderby n.DescriptionC
                                  where new DateTime(n.ClosingDate.Year, n.ClosingDate.Month, n.ClosingDate.Day) >= DateTime.Today  // Sort by name.
                                   && n.AlreadyAssembled < n.RequiredAmount
                                  select n;

            CollectingNameSL = new SelectList(CollectingQuery.AsNoTracking(),
                nameof(Collecting.IdCollecting),
                nameof(Collecting.DescriptionC),
                selectedCollecting);
        }


    }
}
