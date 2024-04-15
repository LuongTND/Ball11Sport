using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data;
using Models;
using Microsoft.AspNetCore.SignalR;
using ProjectFinalWebSale_LuongNewV2.ServiceSystem;
using Microsoft.AspNetCore.Authorization;

namespace ProjectFinalWebSale_LuongNewV2.Pages.Products
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDBContext _context;
    
        public EditModel(ApplicationDBContext context)
        {
            _context = context;
          
        }


        [BindProperty]
        public Product Product { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            

            var product =  await _context.Products.FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            Product = product;
           ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
           ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email");
            return Page();
        }

        [Authorize]
        public async Task<IActionResult> OnPostAsync()
        {


            _context.Attach(Product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                //await _signalRHub.Clients.All.SendAsync("LoadProducts");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(Product.ProductId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ProductExists(int id)
        {
          return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
