using ExpenseTracker.Web.Extensions;
using ExpenseTracker.Web.Models.Contracts;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//                .AddCookie(options =>
//                {
//                    options.Cookie.Name = "MySessionCookie";
//                    options.LoginPath = "/Home/Login";
//                    options.SlidingExpiration = true;
//                });
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddHttpContextAccessor();

builder.Services.AddClient();

builder.Services.AddControllersWithViews();

//builder.Services.AddSingleton<IAppSettings, AppSettings>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
var cookiePolicyOptions = new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.None,
};
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCookiePolicy(cookiePolicyOptions);
app.UseRouting();

app.UseAuthorization();
app.MapDefaultControllerRoute();
app.Run();
