using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using ModelServices;


namespace ProjectFinalWebSale_LuongNewV2.Pages.ShopCarts
{
    public class CartsModel : PageModel
    {
        private readonly Data.ApplicationDBContext _context;

        public CartsModel(Data.ApplicationDBContext context)
        {
            _context = context;
        }

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

        public IActionResult OnGet(int id, int SoLuong, string type = "Normal")
        {
            var myCart = Carts;
            var item = myCart.FirstOrDefault(p => p.MaHh == id);

            if (item == null)//chưa có
            {
                var hangHoa = _context.Products.FirstOrDefault(p => p.ProductId == id);
                item = new CartItem
                {
                    MaHh = id,
                    TenHH = hangHoa.ProductName,
                    DonGia = hangHoa.UnitPrice,
                    SoLuong = SoLuong,
                    Hinh = hangHoa.Images
                };
                myCart.Add(item);
            }
            else
            {
                item.SoLuong += SoLuong;
            }
            HttpContext.Session.Set("GioHang", myCart);

            if (type == "ajax")
            {
                return new JsonResult(new
                {
                    SoLuong = Carts.Sum(c => c.SoLuong)
                });
            }
            return Page();

            //return RedirectToPage("/Index");
        }
    }
}
