using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Persistence;

namespace Application.Repository
{
  /// <summary>
  /// Class Repository
  /// </summary>
  public class Repository : IRepository
  {
    private readonly AppDbContext _ctx;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="ctx">ctx</param>
    public Repository(AppDbContext ctx)
    {
      _ctx = ctx;
    }

    /// <inheritdoc />
    public List<Post> GetAllPosts()
    {
      return _ctx.Posts.ToList();
    }

    /// <inheritdoc />
    public List<Post> GetAllPosts(string category)
    {
      return _ctx
        .Posts
        .Where(post => post.Category.ToLower().Equals(category.ToLower()))
        .ToList();
    }

    /// <inheritdoc />
    public void CreatePost(Post post)
    {
      _ctx.Posts.Add(post);

    }

    /// <inheritdoc />
    public Post ReadPost(int id)
    {
      return _ctx.Posts.FirstOrDefault(post => post.Id == id);
    }

    /// <inheritdoc />
    public void UpdatePost(Post post)
    {
      _ctx.Posts.Update(post);
    }

    /// <inheritdoc />
    public void DeletePost(int id)
    {
      _ctx.Posts.Remove(ReadPost(id));
    }

    /// <inheritdoc />
    public async Task<bool> SaveChangesAsync()
    {
      if (await _ctx.SaveChangesAsync() > 0)
      {
        return true;
      }
      return false;
    }
  }
}
