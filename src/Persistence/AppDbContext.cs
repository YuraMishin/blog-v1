using Domain;
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
    /// <summary>
    /// Posts
    /// </summary>
    public DbSet<Post> Posts { get; set; }
    #endregion
  }
}
