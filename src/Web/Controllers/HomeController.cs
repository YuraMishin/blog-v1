using System.Threading.Tasks;
using Application.Repository;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
  /// <summary>
  /// Сlass HomeController
  /// </summary>
  public class HomeController : Controller
  {
    private readonly IRepository _repo;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="repo">repo</param>
    public HomeController(IRepository repo)
    {
      _repo = repo;
    }

    /// <summary>
    /// Method displays index UI.
    /// GET: /
    /// </summary>
    /// <returns>IActionResult</returns>
    public IActionResult Index()
    {
      var posts = _repo.GetAllPosts();

      return View(posts);
    }

    /// <summary>
    /// Method displays post UI.
    /// GET: /Home/Post
    /// </summary>
    /// <returns>IActionResult</returns>
    public IActionResult Post(int id)
    {
      var post = _repo.ReadPost(id);
      return View(post);
    }

    /// <summary>
    /// Method displays edit post UI.
    /// GET: /Home/Edit
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
    /// POST: /Home/Edit
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
    /// Method removes the post
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
