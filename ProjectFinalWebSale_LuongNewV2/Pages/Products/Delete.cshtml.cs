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
using Microsoft.AspNetCore.Authorization;

namespace ProjectFinalWebSale_LuongNewV2.Pages.Products
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDBContext _context;
    
        public DeleteModel(ApplicationDBContext context)
        {
            _context = context;
       
        }

        [BindProperty]
      public Product Product { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FirstOrDefaultAsync(m => m.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }
            else 
            {
                Product = product;
            }
            return Page();
        }
        [Authorize]
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.FindAsync(id);

            if (product != null)
            {
                Product = product;
                _context.Products.Remove(Product);
                await _context.SaveChangesAsync();
                //await _signalRHub.Clients.All.SendAsync("LoadProducts");
            }

            return RedirectToPage("./Index");
        }
    }
}
