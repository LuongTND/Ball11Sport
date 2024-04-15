using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Data;
using Models;
using Microsoft.AspNetCore.SignalR;
using ProjectFinalWebSale_LuongNewV2.ServiceSystem;
using Microsoft.AspNetCore.Authorization;

namespace ProjectFinalWebSale_LuongNewV2.Pages.Products
{
    public class CreateModel : PageModel
    {
        
        private readonly ApplicationDBContext _context;
    
        public CreateModel(ApplicationDBContext context)
        {
            _context = context;
          
        }
        public IActionResult OnGet()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email");
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; } = default!;


        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> OnPostAsync()
        {
         
            _context.Products.Add(Product);
            await _context.SaveChangesAsync();
            //await _signalRHub.Clients.All.SendAsync("LoadProducts");
            return RedirectToPage("./Index");
        }
    }
}
