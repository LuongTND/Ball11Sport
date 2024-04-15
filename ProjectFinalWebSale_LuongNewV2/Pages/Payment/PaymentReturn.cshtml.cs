using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ModelServices;
using ProjectFinalWebSale_LuongNewV2.ServiceSystem.VnPayLibrary;
using ProjectFinalWebSale_LuongNewV2.ServiceSystem;

namespace ProjectFinalWebSale_LuongNewV2.Pages.Payment
{
    public class PaymentReturnModel : PageModel
    {
        // thanh toan xong trả về cái gì 
        public List<CartItem> Cart => HttpContext.Session.Get<List<CartItem>>(MySetting.CART_KEY) ?? new List<CartItem>();
        private readonly IVnPayService _vnPayservice;
        public PaymentReturnModel(IVnPayService vnPayservice)
        {
            _vnPayservice = vnPayservice;
        }
        // xem bảng mã lỗi : https://sandbox.vnpayment.vn/apis/docs/huong-dan-tich-hop/
        public async Task<IActionResult> OnGetAsync()
        {
            var response = _vnPayservice.PaymentExecute(Request.Query);

            if (response == null || response.VnPayResponseCode != "00")
            {
                TempData["Message"] = $"Lỗi thanh toán VN Pay: {response.VnPayResponseCode}";
                // muốn nhanh thì thêm trang RedirectToPage("./PaymentFail")
                return RedirectToPage("./PaymentFail");
            }


            // Lưu đơn hàng vô database

            TempData["Message"] = $"Thanh toán VNPay thành công";
            // muốn nhanh thì thêm trang RedirectToPage("./PaymentSuccess")
            return RedirectToPage("./PaymentSuccess");
           
        }
    }
}
