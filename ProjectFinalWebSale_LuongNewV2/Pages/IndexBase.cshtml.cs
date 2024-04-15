using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Models;

namespace ProjectFinalWebSale_LuongNewV2.Pages
{
    public class IndexBaseModel : PageModel
    {
        private readonly Data.ApplicationDBContext _context;


        public IndexBaseModel(Data.ApplicationDBContext context)
        {
            _context = context;

        }

        public IList<Product> Product { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Products != null)
            {
                Product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.User).ToListAsync();
            }
        }
    }
}
