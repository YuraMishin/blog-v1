using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Helpers;
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
    public IndexViewModel GetAllPosts(int pageNumber, string category, string search)
    {
      int pageSize = 1;
      int skipAmount = pageSize * (pageNumber - 1);
      var query = _ctx.Posts.AsNoTracking().AsQueryable();

      if (!String.IsNullOrEmpty(category))
      { query = query.Where(post => post.Category.ToLower().Equals(category.ToLower())); }

      if (!String.IsNullOrEmpty(search))
        query = query.Where(x => EF.Functions.Like(x.Title, $"%{search}%")
                                 || EF.Functions.Like(x.Body, $"%{search}%")
                                 || EF.Functions.Like(x.Description, $"%{search}%"));

      int postsCount = query.Count();
      int pageCount = (int)Math.Ceiling((double)postsCount / pageSize);

      return new IndexViewModel
      {
        PageNumber = pageNumber,
        PageCount = pageCount,
        NextPage = postsCount > (skipAmount + pageSize),
        Pages = PageHelper.PageNumbers(pageNumber, pageCount).ToList(),
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
