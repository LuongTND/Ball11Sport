using Data;
using MailKit;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProjectFinalWebSale_LuongNewV2.ServiceSystem;
using ProjectFinalWebSale_LuongNewV2.ServiceSystem.EmailService;
using ProjectFinalWebSale_LuongNewV2.ServiceSystem.VnPayLibrary;
using System.Configuration;
using System.Security.Claims;



var builder = WebApplication.CreateBuilder(args);


builder.Services.AddRazorPages();

//AUTHENTIZATION
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireAssertion(context =>
    {
        var role = context.User.FindFirstValue(ClaimTypes.Role);
        return role == "1";
    }));
});



//AUTHENTICATION 
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
        .AddCookie(options =>
        {
            options.LoginPath = "/Authen/Login";
            options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
            options.SlidingExpiration = true;
            options.AccessDeniedPath = "/Authen/Login";
        })
        .AddGoogle(options =>
        {
            options.ClientId = "641114959725-jf11fal0eqmmjjog40uqpm91brah8mhr.apps.googleusercontent.com";
            options.ClientSecret = "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";
        });


//SESSION
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromDays(7);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});





//DATABASE
builder.Services.AddDbContext<ApplicationDBContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration.GetConnectionString("WebShoppingDatabase"));
});






//SENDMAIL
builder.Services.AddOptions(); 
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.AddTransient<ProjectFinalWebSale_LuongNewV2.ServiceSystem.EmailService.IMailService, SendMailService>();




//VNPAY
builder.Services.AddSingleton<IVnPayService,VnPayService>();






var app = builder.Build();







if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
   
    app.UseHsts();
}

//ADD DU LIEU CUNG
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<ApplicationDBContext>();

    context.Database.EnsureCreated();

    DbInitializer.Initialize(context);
}




app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseSession();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();




app.Run();


//builder.Services.AddAuthentication(options=> 
//{
//    options.DefaultChallengeScheme=GoogleDefaults.AuthenticationScheme;
//}) 
//.AddCookie() .AddGoogle(GoogleDefaults.AuthenticationScheme, options=> {
//   options.ClientId = builder.Configuration.GetSection("GoogleKeys:ClientId").Value;
//   options.ClientSecret = builder.Configuration.GetSection("GoogleKeys:ClientSecret").Value;
//});


//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//    .AddCookie(options =>
//    {
//        options.LoginPath = "/Authen/Login";
//        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
//        options.SlidingExpiration = true;
//        options.AccessDeniedPath = "/Authen/Login";
//    });


//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("CanAccessController", policy =>
//    {
//        policy.RequireClaim(ClaimTypes.Email, "Admin1.RogerShop@gmail.com", "Admin2.RogerShop@gmail.com");
//    });
//});
