var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews();
// Note: Using AddSingleton since your PetService uses an in-memory List
builder.Services.AddSingleton<ShelterHelper.Services.PetService>();

// Add authentication and authorization
builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/Account/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromDays(7);
    });
builder.Services.AddAuthorization();
builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // This handles all your CSS, JS, and Images in .NET 8
app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

// FIX: Removed app.MapStaticAssets() because it is only for .NET 9+

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"); 
    // FIX: Removed .WithStaticAssets() from the end of the route

app.Run();