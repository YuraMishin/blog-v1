using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.FileManager;
using Application.Repository;
using Domain.Comments;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels;

namespace Web.Controllers
{
  /// <summary>
  /// Сlass HomeController
  /// </summary>
  public class HomeController : Controller
  {
    private readonly IRepository _repo;
    private readonly IFileManager _fileManager;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="repo">repo</param>
    /// <param name="fileManager">fileManager</param>
    public HomeController(IRepository repo, IFileManager fileManager)
    {
      _repo = repo;
      _fileManager = fileManager;
    }

    /// <summary>
    /// Method displays index UI.
    /// GET: /
    /// </summary>
    /// <returns>IActionResult</returns>
    public IActionResult Index(string category) =>
      View(string.IsNullOrEmpty(category) ?
        _repo.GetAllPosts() :
        _repo.GetAllPosts(category));


    /// <summary>
    /// Method displays post UI.
    /// GET: /Home/Post
    /// </summary>
    /// <returns>IActionResult</returns>
    public IActionResult Post(int id) =>
      View(_repo.ReadPost(id));

    /// <summary>
    /// Method streams the image.
    /// GET: /Image/{image}
    /// </summary>
    /// <param name="image"></param>
    /// <returns></returns>
    [HttpGet("/Image/{image}")]
    [ResponseCache(CacheProfileName = "Monthly")]
    public IActionResult Image(string image) =>
    new FileStreamResult(
      _fileManager.ImageStream(image),
      $"image/{image.Substring(image.LastIndexOf('.') + 1)}");

    /// <summary>
    /// Method handles comments.
    /// POST: /Home/Comment
    /// </summary>
    /// <param name="commentVM">commentVM</param>
    /// <returns>Task&lt;IActionResult&gt;</returns>
    [HttpPost]
    public async Task<IActionResult> Comment(CommentViewModel commentVM)
    {
      if (!ModelState.IsValid)
        return RedirectToAction("Post", new { id = commentVM.PostId });

      var post = _repo.ReadPost(commentVM.PostId);
      if (commentVM.MainCommentId == 0)
      {
        post.MainComments = post.MainComments ?? new List<MainComment>();

        post.MainComments.Add(new MainComment
        {
          Message = commentVM.Message,
          Created = DateTime.Now,
        });

        _repo.UpdatePost(post);
      }
      else
      {
        var comment = new SubComment
        {
          MainCommentId = commentVM.MainCommentId,
          Message = commentVM.Message,
          Created = DateTime.Now,
        };
        _repo.AddSubComment(comment);
      }

      await _repo.SaveChangesAsync();

      return RedirectToAction("Post", new { id = commentVM.PostId });
    }

  }
}
