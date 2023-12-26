using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

using Microsoft.EntityFrameworkCore;
using System.Linq;

using petsiki.Models;

namespace petsiki.Pages.recom
{
    public class prog : PageModel
    {
        public SelectList UserNameSL { get; set; }

        public void PopulateUsersDropDownList(PettContext _context,
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
        public SelectList PetNameSL { get; set; }
        public void PopulatePetDropDownList(PettContext _context,
            object selectedPet = null)
        {
            var PetQuery = from n in _context.Pets
                           orderby n.Nickname // Sort by name.
                           select n;

            PetNameSL = new SelectList(PetQuery.AsNoTracking(),
                nameof(Pet.IdPet),
                nameof(Pet.Nickname),
                selectedPet);
        }
    }
}






