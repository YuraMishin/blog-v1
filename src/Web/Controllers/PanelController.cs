using System.Threading.Tasks;
using Application.FileManager;
using Application.Repository;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels;

namespace Web.Controllers
{
  /// <summary>
  /// Class PanelController
  /// </summary>
  [Authorize(Roles = "Admin")]
  public class PanelController : Controller
  {
    private readonly IRepository _repo;
    private readonly IFileManager _fileManager;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="repo">repo</param>
    /// <param name="fileManager">fileManager</param>
    public PanelController(IRepository repo, IFileManager fileManager)
    {
      _repo = repo;
      _fileManager = fileManager;
    }

    /// <summary>
    /// Method displays index UI.
    /// GET: /panel
    /// </summary>
    /// <returns>IActionResult</returns>
    public IActionResult Index()
    {
      var posts = _repo.GetAllPosts();

      return View(posts);
    }

    /// <summary>
    /// Method displays edit post UI.
    /// GET: /panel/edit
    /// </summary>
    /// <returns>IActionResult</returns>
    [HttpGet]
    public IActionResult Edit(int? id)
    {
      if (id == null)
      {
        return View(new PostViewModel());
      }
      else
      {
        var post = _repo.ReadPost((int)id);
        return View(new PostViewModel
        {
          Id = post.Id,
          Title = post.Title,
          Body = post.Body,
          CurrentImage = post.Image,
          Description = post.Description,
          Category = post.Category,
          Tags = post.Tags
        });
      }
    }

    /// <summary>
    /// Method handles edit post UI request.
    /// POST: /panel/edit
    /// </summary>
    /// <param name="postVM">postVM</param>
    /// <returns>IActionResult</returns>
    [HttpPost]
    public async Task<IActionResult> Edit(PostViewModel postVM)
    {
      var post = new Post
      {
        Id = postVM.Id,
        Title = postVM.Title,
        Body = postVM.Body,
        Description = postVM.Description,
        Category = postVM.Category,
        Tags = postVM.Tags
      };

      if (postVM.Image == null)
      {
        post.Image = postVM.CurrentImage;
      }
      else
      {
        if (!string.IsNullOrEmpty(postVM.CurrentImage))
        { _fileManager.RemoveImage(postVM.CurrentImage); }
        post.Image = await _fileManager.SaveImage(postVM.Image);
      }

      if (post.Id > 0)
      {
        _repo.UpdatePost(post);
      }
      else
      {
        _repo.CreatePost(post);
      }

      if (await _repo.SaveChangesAsync())
      {
        return RedirectToAction("Index");
      }
      return View(post);
    }

    /// <summary>
    /// Method removes the post.
    /// GET: /panel/remove
    /// </summary>
    /// <param name="id">id</param>
    /// <returns>Task&lt;IActionResult&gt;</returns>
    [HttpGet]
    public async Task<IActionResult> Remove(int id)
    {
      _repo.DeletePost(id);
      await _repo.SaveChangesAsync();
      return RedirectToAction("Index");
    }
  }
}
