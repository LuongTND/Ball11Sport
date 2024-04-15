using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Data;
using Models;

namespace ProjectFinalWebSale_LuongNewV2.Pages.UserDetails
{
    public class CreateModel : PageModel
    {
        private readonly Data.ApplicationDBContext _context;

        public CreateModel(Data.ApplicationDBContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email");
            return Page();
        }

        [BindProperty]
        public UserDetail UserDetail { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          //if (!ModelState.IsValid || _context.UserDetails == null || UserDetail == null)
          //  {
          //      return Page();
          //  }

            _context.UserDetails.Add(UserDetail);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
