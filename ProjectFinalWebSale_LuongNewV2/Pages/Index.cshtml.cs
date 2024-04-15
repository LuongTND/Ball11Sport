using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Microsoft.EntityFrameworkCore;
using Models;
using ProjectFinalWebSale_LuongNewV2.ServiceSystem;

namespace ProjectFinalWebSale_LuongNewV2.Pages
{
    public class IndexModel : PageModel
    {

        private readonly Data.ApplicationDBContext _context;


        public IndexModel(Data.ApplicationDBContext context)
        {
            _context = context;

        }
        [BindProperty(SupportsGet = true)]
        public string? ProductName { get; set; }

        [BindProperty(SupportsGet = true)]
        public decimal? UnitPrice { get; set; }
        public IList<Product> Product { get; set; } = default!;

        public async Task OnGetAsync()
        {
            IQueryable<Product> productsQuery = _context.Products
               .Include(p => p.Category);

            if (!string.IsNullOrEmpty(ProductName))
            {
                productsQuery = productsQuery.Where(p => EF.Functions.Like(p.ProductName, $"%{ProductName}%"));
            }

            if (UnitPrice != null)
            {
                // Assuming UnitPrice is a string in the search form
                //double.TryParse(UnitPrice, out double unitPrice);
                productsQuery = productsQuery.Where(p => p.UnitPrice == UnitPrice);
            }


            Product = await productsQuery.ToListAsync();

            //if (_context.Products != null)
            //{
            //    Product = await _context.Products
            //    .Include(p => p.Category)
            //    .Include(p => p.User).ToListAsync();
            //}

        }
        public string ToVnd(decimal amount)
        {
            return $"{amount:N0} VND"; // Sử dụng chuỗi format "N0" để định dạng số với phân cách hàng nghìn và không có số lẻ
        }
    }
}
