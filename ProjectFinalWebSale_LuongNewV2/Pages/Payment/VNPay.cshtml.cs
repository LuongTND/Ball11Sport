using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Models;
using ModelServices;
using ProjectFinalWebSale_LuongNewV2.ServiceSystem;
using ProjectFinalWebSale_LuongNewV2.ServiceSystem.VnPayLibrary;

namespace ProjectFinalWebSale_LuongNewV2.Pages.Payment
{
    public class VNPayModel : PageModel
    {
        private readonly ApplicationDBContext _context;
        private readonly IVnPayService _vnPayservice;
        public VNPayModel(ApplicationDBContext context, IVnPayService vnPayservice)
        {
            _context = context;
            _vnPayservice = vnPayservice;
        }
        public List<CartItem> Cart => HttpContext.Session.Get<List<CartItem>>(MySetting.CART_KEY) ?? new List<CartItem>();
        //public List<CartItem> Carts
        //{
        //    get
        //    {
        //        var data = HttpContext.Session.Get<List<CartItem>>("GioHang");
        //        if (data == null)
        //        {
        //            data = new List<CartItem>();
        //        }
        //        return data;
        //    }
        //}
        //public IList<User> User { get; set; } = default!;
        public IActionResult OnGet()
        {
            var vnPayModel = new VnPaymentRequestModel
            {
                Amount = (double)Cart.Sum(p => p.ThanhTien),
                CreatedDate = DateTime.Now,
                //Description = $"{model.HoTen} {model.DienThoai}",
                Description = $"test",
                //FullName = model.HoTen,
                FullName = "test",
                OrderId = new Random().Next(1000, 100000)
            };
            return Redirect(_vnPayservice.CreatePaymentUrl(HttpContext, vnPayModel));
        }
    }
}
