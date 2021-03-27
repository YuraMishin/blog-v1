using Application.Repository;
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
  }
}
