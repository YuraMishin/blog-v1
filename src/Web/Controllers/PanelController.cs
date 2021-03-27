using System.Threading.Tasks;
using Application.Repository;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
  /// <summary>
  /// Class PanelController
  /// </summary>
  [Authorize(Roles = "Admin")]
  public class PanelController : Controller
  {
    private readonly IRepository _repo;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="repo">repo</param>
    public PanelController(IRepository repo)
    {
      _repo = repo;
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
        return View(new Post());
      }
      var post = _repo.ReadPost((int)id);
      return View(post);
    }

    /// <summary>
    /// Method handles edit post UI request.
    /// POST: /panel/edit
    /// </summary>
    /// <param name="post">post</param>
    /// <returns>IActionResult</returns>
    [HttpPost]
    public async Task<IActionResult> Edit(Post post)
    {
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
