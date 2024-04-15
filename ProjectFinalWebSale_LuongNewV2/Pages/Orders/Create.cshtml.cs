using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Data;
using Models;
using Microsoft.AspNetCore.Authorization;

namespace ProjectFinalWebSale_LuongNewV2.Pages.Orders
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
        public Order Order { get; set; } = default!;


        [Authorize]
        public async Task<IActionResult> OnPostAsync()
        {
          //if (!ModelState.IsValid || _context.Orders == null || Order == null)
          //  {
          //      return Page();
          //  }

            _context.Orders.Add(Order);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
