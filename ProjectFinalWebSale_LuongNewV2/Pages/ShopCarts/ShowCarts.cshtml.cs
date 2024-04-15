using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using ModelServices;

namespace ProjectFinalWebSale_LuongNewV2.Pages.ShopCarts
{
    public class ShowCartsModel : PageModel
    {
        private readonly Data.ApplicationDBContext _context;

        public ShowCartsModel(Data.ApplicationDBContext context)
        {
            _context = context;
        }
        [BindProperty]
        public IList<User> User { get; set; } = default!;

        public List<CartItem> Carts
        {
            get
            {
                var data = HttpContext.Session.Get<List<CartItem>>("GioHang");
                if (data == null)
                {
                    data = new List<CartItem>();
                }
                return data;
            }
        }
        public void OnGet()
        {
        }
    }
}
