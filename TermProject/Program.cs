using Microsoft.EntityFrameworkCore;
using TermProject.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//adding in the DbContext
builder.Services.AddDbContext<TournamentDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("TournamentDb"))
);


//For login: Add authentication and Authorization services
builder.Services.AddAuthentication("app-cookie")
    .AddCookie("app-cookie", options =>
    {
        options.LoginPath = "/Auth/Index";
        options.LogoutPath = "/Auth/Logout";
        options.AccessDeniedPath = "/Auth/Denied";
    });

builder.Services.AddAuthorization();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
