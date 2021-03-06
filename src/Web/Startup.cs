using Application.FileManager;
using Application.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Persistence;
using Web.Configuration;
using Web.Services.Email;

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
      services.Configure<SmtpSettings>(_configuration.GetSection("SmtpSettings"));

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

      #region Auth
      services.AddIdentity<IdentityUser, IdentityRole>(options =>
        {
          options.Password.RequireDigit = false;
          options.Password.RequireLowercase = true;
          options.Password.RequireNonAlphanumeric = false;
          options.Password.RequireUppercase = false;
          options.Password.RequiredLength = 1;
        })
        .AddEntityFrameworkStores<AppDbContext>();
      services.ConfigureApplicationCookie(options =>
      {
        options.LoginPath = "/Auth/Login";
      });
      #endregion

      #region Dependencies Injection
      services.AddTransient<IRepository, Repository>();
      services.AddTransient<IFileManager, FileManager>();
      services.AddSingleton<IEmailService, EmailService>();
      #endregion

      services.AddMvc(options =>
      {
        options.CacheProfiles.Add("Monthly", new CacheProfile { Duration = 60 * 60 * 24 * 7 * 4 });
      });

      services.AddControllersWithViews();
      services.AddRazorPages().AddRazorRuntimeCompilation();
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
        app.UseDeveloperExceptionPage();
      }

      app.UseStaticFiles();
      app.UseRouting();

      // Auth
      app.UseAuthentication();
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllerRoute(
          name: "default",
          pattern: "{controller=Home}/{action=Index}/{id?}");
        endpoints.MapRazorPages();
      });

    }
  }
}
