using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Persistence;

namespace Web.Controllers
{
  /// <summary>
  /// Сlass HomeController
  /// </summary>
  public class HomeController : Controller
  {
    private readonly AppDbContext _ctx;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="ctx">ctx</param>
    public HomeController(AppDbContext ctx)
    {
      _ctx = ctx;
    }

    /// <summary>
    /// Method displays index UI.
    /// GET: /
    /// </summary>
    /// <returns>IActionResult</returns>
    public IActionResult Index()
    {
      return View();
    }

    /// <summary>
    /// Method displays post UI.
    /// GET: /Home/Post
    /// </summary>
    /// <returns>IActionResult</returns>
    public IActionResult Post()
    {
      return View();
    }

    /// <summary>
    /// Method displays edit post UI.
    /// GET: /Home/Edit
    /// </summary>
    /// <returns>IActionResult</returns>
    [HttpGet]
    public IActionResult Edit()
    {
      return View(new Post());
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
      _ctx.Posts.Add(post);
      await _ctx.SaveChangesAsync();

      return RedirectToAction("Index");
    }
  }
}
