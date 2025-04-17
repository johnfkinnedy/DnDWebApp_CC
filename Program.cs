using DnDWebApp_CC.Models;
using DnDWebApp_CC.Services;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;


namespace DnDWebApp_CC
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<ApplicationDbContext>(options => {
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            // Allow postman to send requests
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                builder =>
                {
                    builder.WithOrigins("https://web.postman.co")
                .AllowAnyHeader()
                .AllowAnyMethod();
                });
            });

            builder.Services.AddScoped<DataInitializer>();
            builder.Services.AddScoped<IBackgroundRepository, BackgroundRepository>();
            builder.Services.AddScoped<ICharacterClassRepository, CharacterClassRepository>();
            builder.Services.AddScoped<IDiceRepository, DiceRepository>();
            builder.Services.AddScoped<IEquipmentRepository, EquipmentRepository>();
            builder.Services.AddScoped<ISkillRepository, SkillRepository>();
            builder.Services.AddScoped<ISpeciesRepository, SpeciesRepository>();
            builder.Services.AddScoped<ISpellRepository, SpellRepository>();
            builder.Services.AddScoped<IStatRepository, StatRepository>();

            //services here
            var app = builder.Build();
            await SeedDataAsync(app);

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            static async Task SeedDataAsync(WebApplication app)
            {
                //Seed database
                using (var scope = app.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    try
                    {
                        var initializer = services.GetRequiredService<DataInitializer>();
                        await initializer.SeedDiceAsync();
                        await initializer.SeedStatsAsync();
                        await initializer.SeedSpellsAsync();
                        await initializer.SeedSkillsAsync();
                        await initializer.SeedSpeciesAsync();
                        await initializer.SeedBackgroundsAsync();
                        await initializer.SeedClassesAsync();
                    }
                    catch (Exception ex)
                    {
                        var logger = services.GetRequiredService<ILogger<Program>>();
                        logger.LogError(ex, "An error occurred while seeding the database.");
                    }
                }
            }
                app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
