using System;
using CI_PLATFOEM_REPOSITORY.Repository;
using CI_PLATFORM_REPOSITORY.DataDB;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(c=>c.LoginPath="/UserAuthentication/Login");
builder.Services.AddDbContext<CI_PLATFORM_REPOSITORY.DataDB.CiPlatformContext>(
        options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection"));

    builder.Services.AddScoped<IRepository<Task>,IRepository<Task>>();



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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=UserAuthentication}/{action=login}");

app.Run();
