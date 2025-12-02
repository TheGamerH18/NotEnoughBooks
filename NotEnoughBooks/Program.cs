using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NotEnoughBooks.Adapters;
using NotEnoughBooks.Core;
using NotEnoughBooks.Core.Ports;
using NotEnoughBooks.Data;
using NotEnoughBooks.Parser.DNB;

namespace NotEnoughBooks;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(connectionString));
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<ApplicationDbContext>();
        builder.Services.AddControllersWithViews();
        
        builder.Services.AddHttpClient();

        builder.Services.AddScoped<ISaveBookPort, SaveBookAdapter>();
        builder.Services.AddScoped<IGetBooksByUserPort, GetBooksByUserAdapter>();
        builder.Services.AddScoped<ICacheThumbnailPort, CacheThumbnailAdapter>();
        builder.Services.AddScoped<IGetBookByIdPort, GetBookByIdAdapter>();
        
        builder.Services.AddCore();
        builder.Services.AddDnbParser();
 
        WebApplication app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
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
        app.MapRazorPages();

        app.Run();
    }
}
