using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using petsk.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace petsk.Pages.Coll
{
    public class Collec:PageModel
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
        public SelectList ShelterNameSL { get; set; }
       
        public void PopulateShelterDropDownList(PettContext _context,
            object selectedShelter = null)
        {
            var ShelterQuery = from n in _context.Shelters
                                  orderby n.IdShelter
                                  select n;

            ShelterNameSL = new SelectList(ShelterQuery.AsNoTracking(),
                nameof(Shelter.IdShelter),
                nameof(Shelter.Address),
                selectedShelter);



        }

        public SelectList PetNameSL { get; set; }
        public void PopulatePetDropDownList(PettContext _context,
            object selectedPet = null)
        {
            var PetQuery = from n in _context.Pets
                                  orderby n.IdPet
                                  select n;

            PetNameSL = new SelectList(PetQuery.AsNoTracking(),
                nameof(Pet.IdPet),
                nameof(Pet.Nickname),
                selectedPet);
           


        }
    }
}
//using petsik.Data;




