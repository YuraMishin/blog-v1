using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Persistence;

namespace Web
{
  /// <summary>
  /// Class Startup.
  /// Implements initial settings
  /// </summary>
  public class Startup
  {
    /// <summary>
    /// Configuration
    /// </summary>
    private readonly IConfiguration _configuration;

    /// <summary>
    /// 
    /// </summary>
    private readonly IWebHostEnvironment _env;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="configuration">configuration</param>
    /// <param name="env">env</param>
    public Startup(IConfiguration configuration, IWebHostEnvironment env)
    {
      _configuration = configuration;
      _env = env;
    }

    /// <summary>
    /// Method gets called by the runtime to add services to the container
    /// </summary>
    /// <param name="services">services</param>
    public void ConfigureServices(IServiceCollection services)
    {
      #region DB connection
      services.AddDbContext<AppDbContext>(
        option =>
        {
          option.EnableSensitiveDataLogging();
          option.EnableDetailedErrors();
          option.UseNpgsql(
            _configuration.GetConnectionString("MyBlog"));
        });
      #endregion

      services.AddControllersWithViews();
    }

    /// <summary>
    /// Method gets called by the runtime to configure the HTTP request pipeline
    /// </summary>
    /// <param name="app">app</param>
    /// <param name="logger">logger</param>
    public void Configure(IApplicationBuilder app, ILogger<Startup> logger)
    {
      logger.LogInformation("App is running. Enjoy!");

      if (_env.IsDevelopment())
      {
        logger.LogInformation("Development mode");
        app.UseDeveloperExceptionPage();
      }

      if (_env.IsProduction())
      {
        logger.LogInformation("Production mode");
      }

      app.UseStaticFiles();
      app.UseRouting();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllerRoute(
          name: "default",
          pattern: "{controller=Home}/{action=Index}/{id?}");
      });

    }
  }
}
