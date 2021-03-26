using Microsoft.EntityFrameworkCore;

namespace Persistence
{
  /// <summary>
  /// Class AppDbContext
  /// </summary>
  public class AppDbContext : DbContext
  {
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="options">options</param>
    public AppDbContext(DbContextOptions<AppDbContext> options)
      : base(options)
    { }

    #region DB Tables

    #endregion
  }
}
