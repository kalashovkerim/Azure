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
using NimbRepository.Model.Seller;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("NimbDbContextConnection") ?? throw new InvalidOperationException("Connection string 'NimbDbContextConnection' not found.");

var services = builder.Services;

services.AddControllersWithViews();

services.AddDbContext<NimbDbContext>();


services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "AuthToken";
        options.ExpireTimeSpan = TimeSpan.FromHours(5);
        options.SlidingExpiration = true;
        options.AccessDeniedPath = new PathString("/ErrorPages/AccessDenied");
        options.LogoutPath = "/logout";
    });


services.AddScoped<IUnitOfWork, UnitOfWork>();

services.AddMvc();

services.AddScoped<IValidator<User>, UserValidator>();
services.AddScoped<IValidator<Good>, GoodValidator>();
services.AddScoped<IValidator<Supplier>, SupplierValidator>();
services.AddScoped<IValidator<Client>, ClientValidator>();

var app = builder.Build();

app.UseDeveloperExceptionPage();

if (!app.Environment.IsDevelopment())
{
    
    app.UseHsts();
}

if (app.Environment.IsDevelopment())
{

    app.UseExceptionHandler("/error/404");
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}");

/*app.Use(async (ctx, next) =>
{
    await next();

    if (ctx.Response.StatusCode == 404 && !ctx.Response.HasStarted)
    {
        //Re-execute the request so the user gets the error page
        string originalPath = ctx.Request.Path.Value;
        ctx.Items["originalPath"] = originalPath;
        ctx.Request.Path = "/error/404";
        await next();
    }
});*/

app.Run();
