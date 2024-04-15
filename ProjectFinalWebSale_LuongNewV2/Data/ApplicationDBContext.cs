using Microsoft.EntityFrameworkCore;
using Models;

namespace Data
{
    public partial class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext() { }
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserDetail> UserDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Role
            modelBuilder.Entity<Role>()
                .HasKey(r => r.RoleId);

            // User
            modelBuilder.Entity<User>()
                .HasKey(u => u.UserId);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany()
                .HasForeignKey(u => u.RoleId);
                

            // UserDetail
            modelBuilder.Entity<UserDetail>()
                .HasKey(ud => ud.Id);

            modelBuilder.Entity<UserDetail>()
               .HasOne(ud => ud.User)
              
               .WithMany()
               .HasForeignKey(ud => ud.UserId);


            // Product
            modelBuilder.Entity<Product>()
                .HasKey(p => p.ProductId);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.User)
                //.WithMany(u => u.Products)
                .WithMany()
                .HasForeignKey(p => p.UserId);


            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                //.WithMany(c => c.Products)
                .WithMany()
                .HasForeignKey(p => p.CategoryId);
             

            // Category
            modelBuilder.Entity<Category>()
                .HasKey(c => c.CategoryId);

            // Order
            modelBuilder.Entity<Order>()
                .HasKey(o => o.OrderId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                //.WithMany(u => u.Orders)
                .WithMany()
                .HasForeignKey(o => o.UserId);
         

            // OrderDetail
            modelBuilder.Entity<OrderDetail>()
                .HasKey(od => od.OrderDetailId);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Order)
                //.WithMany(o => o.OrderDetails)
                .WithMany()
                .HasForeignKey(od => od.OrderId);


            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Product)
                //.WithMany(p => p.OrderDetails)
                .WithMany()
                .HasForeignKey(od => od.ProductId)
                .OnDelete(DeleteBehavior.NoAction);



        }


    }
}
