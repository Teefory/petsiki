using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using petsk.Models;

namespace petsk.Pages.us
{
    public class CreateModel : PageModel
    {
        private readonly petsk.Models.PettContext _context;

        public CreateModel(petsk.Models.PettContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Users User { get; set; } = default!;

        public class HashPasswordClass
        {
            public static string HashPassword(string password)
            {
                using (var md5 = MD5.Create())
                {
                    var hashedBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
                    var hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                    return hash;
                }
            }
        }
        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            User.Password = HashPasswordClass.HashPassword(User.Password);
            if (!ModelState.IsValid || _context.Users == null || User == null)
            {
                return Page();
            }

            var userExists = await _context.Users.AnyAsync(x => x.Login == User.Login);
            if (userExists)
            {
                // Добавляем кастомное сообщение об ошибке
                ModelState.AddModelError("", "Пользователь с таким логином уже существует.");
                return Page();
            }

            _context.Users.Add(User);
            await _context.SaveChangesAsync();

            return RedirectToPage("/us/CreateEnd");
        }
    }
}
