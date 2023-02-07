using System.Security.Principal;
using NimbRepository.DbContexts;
using NimbRepository.Repository.Classes;
using NimbRepository.Repository.Interfaces;
using NimbRepository.Model.Admin;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using System;
using NimbRepository.Validators.Classes;
using NimbRepository.Model.Storekeeper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("NimbDbContextConnection") ?? throw new InvalidOperationException("Connection string 'NimbDbContextConnection' not found.");

var services = builder.Services;

services.AddControllersWithViews();

services.AddDbContext<NimbDbContext>();


services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "AuthToken";
        options.ExpireTimeSpan = TimeSpan.FromSeconds(5);
        options.SlidingExpiration = true;
        options.AccessDeniedPath = "/Forbidden/";
    });


//services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<NimbDbContext>();

services.AddScoped<IUnitOfWork, UnitOfWork>();

services.AddMvc();

services.AddScoped<IValidator<User>, UserValidator>();
services.AddScoped<IValidator<Good>, GoodValidator>();
services.AddScoped<IValidator<Supplier>, SupplierValidator>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}");

app.Run();
