using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ProjectFinalWebSale_LuongNewV2.Pages.Payment
{
    public class PaymentFailModel : PageModel
    {
        public async Task<IActionResult> OnGetAsync()
        {
            return Page();
        }
    }
}
