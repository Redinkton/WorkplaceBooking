using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.EntityFrameworkCore;
using WorkplaceBooking;
using WorkplaceBooking.Data;
using WorkplaceBooking.Middleware;
using WorkplaceBooking.Repositories;
using WorkplaceBooking.Services;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options
    .UseSqlite("Data Source=seatbooking.db")
    //.UseLazyLoadingProxies()
    );

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<ISeatService, SeatService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ISeatRepository, SeatRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAdminService, AdminService>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
.AddCookie()
.AddGoogle(options =>
{
    options.ClientId = builder.Configuration["GoogleKeys:ClientId"];
    options.ClientSecret = builder.Configuration["GoogleKeys:ClientSecret"];
    options.Events.OnCreatingTicket = async context =>
    {
        var userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
        await userService.RegisterUserAsync(context.Principal);

        var userId = context.Principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!string.IsNullOrEmpty(userId))
        {
            bool isAdmin = await userService.IsAdminAsync(userId);
            if (isAdmin)
            {
                context.Identity.AddClaim(new Claim("IsAdmin", "true"));
            }
        }

    };
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireClaim("IsAdmin", "true"));
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<SeatMiddleware>();
app.UseStaticFiles();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();