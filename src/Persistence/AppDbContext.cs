using Domain;
using Domain.Comments;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
  /// <summary>
  /// Class AppDbContext
  /// </summary>
  public class AppDbContext : IdentityDbContext
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

    /// <summary>
    /// MainComments
    /// </summary>
    public DbSet<MainComment> MainComments { get; set; }

    /// <summary>
    /// SubComments
    /// </summary>
    public DbSet<SubComment> SubComments { get; set; }
    #endregion
  }
}
