using Models;

namespace Data
{
    public static  class DbInitializer
    {
        public static void Initialize(ApplicationDBContext context)
        {
            //Roler
            if (!context.Users.Any())
            {
                List<Role> roles = new List<Role>
                {
                    new Role { RoleName="Admin" },
                    new Role {  RoleName = "Customer" },
                  
                };
                context.Roles.AddRange(roles);
                context.SaveChanges();
            }
            //Users
            if (!context.Users.Any())
            {
                List<User> users = new List<User>
                {
                    new User { UserName = "Admin1", Password = "123456", RoleId = 1, Email = "Admin1.RogerShop@gmail.com" },
                    new User { UserName = "Admin2", Password = "123456", RoleId=1,Email = "Admin2.RogerShop@gmail.com" },
                    new User { UserName = "Customer1", Password = "123456", RoleId = 2,Email = "Customer1.RogerShop@gmail.com" },
                    new User { UserName = "Customer2", Password = "123456",  RoleId = 2 ,Email = "Customer2.RogerShop@gmail.com"},
                };
                context.Users.AddRange(users);
                context.SaveChanges();
            }
            // categories
            if (!context.Categories.Any())
            {
                List<Category> categories = new List<Category>
                {
                    new Category { CategoryName = "Football", Status= true },
                    new Category { CategoryName = "Tennis", Status= true }
                };

                context.Categories.AddRange(categories);
                context.SaveChanges();
            }
            //Products
            if (!context.Products.Any())
            {
                List<Product> products = new List<Product>
                {
                    new Product { ProductName = "Ball", CategoryId=1 , UserId= 1, UnitStock =1, UnitPrice=200000, Images="/img/traibongNHA.jpg" , Status=true },
                   
                };
                context.Products.AddRange(products);
                context.SaveChanges();
            }
        }
    }
}
