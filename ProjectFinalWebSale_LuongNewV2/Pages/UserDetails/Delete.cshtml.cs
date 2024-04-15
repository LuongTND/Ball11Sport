using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Data;
using Models;

namespace ProjectFinalWebSale_LuongNewV2.Pages.UserDetails
{
    public class DeleteModel : PageModel
    {
        private readonly Data.ApplicationDBContext _context;

        public DeleteModel(Data.ApplicationDBContext context)
        {
            _context = context;
        }

        [BindProperty]
      public UserDetail UserDetail { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.UserDetails == null)
            {
                return NotFound();
            }

            var userdetail = await _context.UserDetails.FirstOrDefaultAsync(m => m.Id == id);

            if (userdetail == null)
            {
                return NotFound();
            }
            else 
            {
                UserDetail = userdetail;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.UserDetails == null)
            {
                return NotFound();
            }
            var userdetail = await _context.UserDetails.FindAsync(id);

            if (userdetail != null)
            {
                UserDetail = userdetail;
                _context.UserDetails.Remove(UserDetail);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
