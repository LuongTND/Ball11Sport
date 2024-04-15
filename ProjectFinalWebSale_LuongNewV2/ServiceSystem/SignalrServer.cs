using Data;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace ProjectFinalWebSale_LuongNewV2.ServiceSystem
{
    public class SignalrServer : Hub
    {
        private readonly ApplicationDBContext _context;
        public SignalrServer(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task DeleteProduct(int? deleteId)
        {
            if (deleteId != null)
            {
                var product = _context.Products
                     .FirstOrDefault(x => x.ProductId == deleteId);
           
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
            await Clients.All.SendAsync("Loadproduct", deleteId);
        }
    }
}
