using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Models;

namespace ProjectFinalWebSale_LuongNewV2.Pages.ShopCarts
{
    public class OrderAuthenModel : PageModel
    {
        private readonly Data.ApplicationDBContext _context;

        public OrderAuthenModel(Data.ApplicationDBContext context)
        {
            _context = context;
        }
        public UserDetail UserDetail { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            

            var userdetail = await _context.UserDetails.FirstOrDefaultAsync(m => m.UserId == id);
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
    }
}
