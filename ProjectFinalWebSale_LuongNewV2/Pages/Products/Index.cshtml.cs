using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Data;
using Models;
using Microsoft.AspNetCore.SignalR;
using ProjectFinalWebSale_LuongNewV2.ServiceSystem;

namespace ProjectFinalWebSale_LuongNewV2.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDBContext _context;

        public IndexModel(ApplicationDBContext context)
        {
            _context = context;
    
        }

        public IList<Product> Product { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Products != null)
            {
                Product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.User).ToListAsync();
                //await _signalRHub.Clients.All.SendAsync("LoadProducts");
            }
        }
    }
}
