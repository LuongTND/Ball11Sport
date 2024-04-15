using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Data;
using Models;
using Microsoft.EntityFrameworkCore;

namespace ProjectFinalWebSale_LuongNewV2.Pages.Authen
{
    public class CreateModel : PageModel
    {
        private readonly Data.ApplicationDBContext _context;

        public CreateModel(Data.ApplicationDBContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleName");
            return Page();
        }

        [BindProperty]
        public User User { get; set; } 
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public IActionResult OnPost()
        {
            try
            {
                //if (!ModelState.IsValid)
                //{
                //    return Page();
                //}

                var a =  _context.Users.FirstOrDefault(x => x.UserName == User.UserName);
                if (a != null)
                {   // Email đã tồn tại, xử lý logic tương ứng
                    ModelState.AddModelError(string.Empty, "Email is already registered.");
                    return RedirectToPage();
                }

                // Kiểm tra xem email đã tồn tại hay chưa
                var existingUser =  _context.Users.FirstOrDefault(a => a.Email == User.Email);

                if (existingUser != null)
                {
                    // Email đã tồn tại, xử lý logic tương ứng
                    ModelState.AddModelError(string.Empty, "Email is already registered.");
                    return Page();
                }


                // Email chưa tồn tại, thêm người dùng mới vào cơ sở dữ liệu
                _context.Users.Add(User);
                _context.SaveChanges();

                // Redirect hoặc thực hiện các hành động khác sau khi đăng ký thành công
                return RedirectToPage("./Login");
            }
            catch (Exception ex)
            {
                return RedirectToPage();
            }


            // CODE DỰ PHÒNG

            //if (!ModelState.IsValid || _context.Users == null || User == null)
            //  {
            //      return Page();
            //  }

            //  _context.Users.Add(User);
            //  await _context.SaveChangesAsync();

            //  return RedirectToPage("./Index");
        }
    }
}
