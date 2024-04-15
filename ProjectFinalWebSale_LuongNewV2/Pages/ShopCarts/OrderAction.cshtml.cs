using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;

namespace ProjectFinalWebSale_LuongNewV2.Pages.ShopCarts
{
    public class OrderActionModel : PageModel
    {
        private readonly Data.ApplicationDBContext _context;

        public OrderActionModel(Data.ApplicationDBContext context)
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


        
        public async Task<IActionResult> OnPostAsync()
        {

            // Thêm code cho việc add một list Order mới , Oderdate = thời gian hiện tại , UserId =  (var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId");
            //var userId = userIdClaim?.Value;
            //int id = int.Parse(userId);), status = true

            
                var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId");
                if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
                {
                    Order.OrderDate = DateTime.Now;
                    Order.UserId = userId;
                    Order.Status = true;

                    _context.Orders.Add(Order);
                    await _context.SaveChangesAsync();

                    
                }

            return RedirectToPage("./MailOrder");
        }
    }
}
