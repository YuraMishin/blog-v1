using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Persistence;

namespace Web
{
  /// <summary>
  /// Class Program.
  /// Implements the main app class
  /// </summary>
  public class Program
  {
    /// <summary>
    /// App entry point
    /// </summary>
    /// <param name="args">args</param>
    /// <returns>Task</returns>
    public static async Task Main(string[] args)
    {
      var host = CreateHostBuilder(args).Build();
      using var scope = host.Services.CreateScope();

      var services = scope.ServiceProvider;
      try
      {
        var context = services.GetRequiredService<AppDbContext>();
        var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        // Create DB if it doesn't exist
        //context.Database.EnsureCreated();
        await context.Database.MigrateAsync();

        if (env.IsDevelopment())
        {
          var adminRole = new IdentityRole("Admin");
          // Seed data
          if (!context.Roles.Any())
          {
            // create a role
            await roleManager.CreateAsync(adminRole);
          }

          if (!context.Users.Any(user => user.UserName == "admin"))
          {
            // create an admin
            var adminUser = new IdentityUser
            {
              UserName = "admin",
              Email = "admin@mail.ru"
            };
            var result = await userManager.CreateAsync(adminUser, "q");
            // add role to user + alter to await =)
            userManager.AddToRoleAsync(adminUser, adminRole.Name).GetAwaiter().GetResult();
          }

        }
      }
      catch (Exception ex)
      {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occured during migration");
      }

      await host.RunAsync();
    }

    /// <summary>
    /// Method bootstraps the app
    /// </summary>
    /// <param name="args">args</param>
    /// <returns>IHostBuilder</returns>
    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
              webBuilder.UseStartup<Startup>();
            });
  }
}
