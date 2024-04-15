using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ModelServices;
using ProjectFinalWebSale_LuongNewV2.ServiceSystem;
using ProjectFinalWebSale_LuongNewV2.ServiceSystem.VnPayLibrary;

namespace ProjectFinalWebSale_LuongNewV2.Pages.Payment
{
    public class PaymentModel : PageModel
    {
        //quá trình thanh toán
        public List<CartItem> Cart => HttpContext.Session.Get<List<CartItem>>(MySetting.CART_KEY) ?? new List<CartItem>();
        private readonly IVnPayService _vnPayservice;
        public PaymentModel(IVnPayService vnPayservice)
        {
            _vnPayservice = vnPayservice;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            var vnPayModel = new VnPaymentRequestModel
            {
                Amount = (double)Cart.Sum(p => p.ThanhTien),
                CreatedDate = DateTime.Now,
                Description = "VN pay",
                FullName = "Test",
                OrderId = new Random().Next(1000, 100000)
            };
            return Redirect(_vnPayservice.CreatePaymentUrl(HttpContext, vnPayModel));
        }
    }
}
