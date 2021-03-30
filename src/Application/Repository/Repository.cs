using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.ViewModels;
using Domain;
using Domain.Comments;
using Microsoft.EntityFrameworkCore;
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
    public IndexViewModel GetAllPosts(int pageNumber, string category)
    {
      int pageSize = 1;
      int skipAmount = pageSize * (pageNumber - 1);
      var query = _ctx.Posts.AsQueryable();

      if (!String.IsNullOrEmpty(category))
      { query = query.Where(post => post.Category.ToLower().Equals(category.ToLower())); }

      int postsCount = query.Count();

      return new IndexViewModel
      {
        PageNumber = pageNumber,
        PageCount = (int)Math.Ceiling((double)postsCount / pageSize),
        NextPage = postsCount > (skipAmount + pageSize),
        Category = category,
        Posts = query
          .Skip(skipAmount)
          .Take(pageSize)
          .ToList()
      };
    }

    /// <inheritdoc />
    public void CreatePost(Post post)
    {
      _ctx.Posts.Add(post);

    }

    /// <inheritdoc />
    public Post ReadPost(int id)
    {
      return _ctx.Posts
        .Include(p => p.MainComments)
        .ThenInclude(mc => mc.SubComments)
        .FirstOrDefault(p => p.Id == id);
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

    /// <inheritdoc />
    public void AddSubComment(SubComment comment)
    {
      _ctx.SubComments.Add(comment);
    }
  }
}
