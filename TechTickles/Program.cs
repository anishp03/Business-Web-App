using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSession();

builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("MySQLConnection"),
                     ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("MySQLConnection"))));


var app = builder.Build();

try
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        dbContext.Database.OpenConnection(); // Try opening a connection to the DB
        dbContext.Database.CloseConnection(); // Close the connection after testing
        Console.WriteLine("Database connection successful.");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Error connecting to the database: {ex.Message}");
    throw; // Optionally rethrow the exception to stop the app from starting
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
