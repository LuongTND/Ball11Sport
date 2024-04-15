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
    public class IndexModel : PageModel
    {
        private readonly Data.ApplicationDBContext _context;

        public IndexModel(Data.ApplicationDBContext context)
        {
            _context = context;
        }

        public IList<UserDetail> UserDetail { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.UserDetails != null)
            {
                UserDetail = await _context.UserDetails
                .Include(u => u.User).ToListAsync();
            }
        }
    }
}
