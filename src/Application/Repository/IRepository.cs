using System.Collections.Generic;
using System.Threading.Tasks;
using Application.ViewModels;
using Domain;
using Domain.Comments;

namespace Application.Repository
{
  /// <summary>
  /// Interface IRepository
  /// </summary>
  public interface IRepository
  {
    /// <summary>
    /// Method gets all the posts
    /// </summary>
    /// <returns>List&lt;Post&gt;</returns>
    List<Post> GetAllPosts();

    /// <summary>
    /// Method gets all posts with pagination
    /// </summary>
    /// <param name="pageNumber">pageNumber</param>
    /// <param name="category">category</param>
    /// <param name="search">search</param>
    /// <returns></returns>
    IndexViewModel GetAllPosts(int pageNumber, string category, string search);

    /// <summary>
    /// Method creates a post
    /// </summary>
    /// <param name="post">post</param>
    void CreatePost(Post post);

    /// <summary>
    /// Method reads the post
    /// </summary>
    /// <param name="id">id</param>
    /// <returns>Post</returns>
    Post ReadPost(int id);

    /// <summary>
    /// Method updates the post
    /// </summary>
    /// <param name="post">post</param>
    void UpdatePost(Post post);

    /// <summary>
    /// Method deletes the post
    /// </summary>
    /// <param name="id">id</param>
    void DeletePost(int id);

    /// <summary>
    /// Method saves changes
    /// </summary>
    /// <returns>Task&lt;bool&gt;</returns>
    Task<bool> SaveChangesAsync();

    /// <summary>
    /// Method adds subcomment
    /// </summary>
    /// <param name="comment">comment</param>
    void AddSubComment(SubComment comment);


  }
}
