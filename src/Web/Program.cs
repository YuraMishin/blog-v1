using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

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
    public static void Main(string[] args)
    {
      CreateHostBuilder(args).Build().Run();
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
