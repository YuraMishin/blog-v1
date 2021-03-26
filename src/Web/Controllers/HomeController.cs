using Domain;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
  /// <summary>
  /// Сlass HomeController
  /// </summary>
  public class HomeController : Controller
  {
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
    public IActionResult Edit(Post post)
    {
      return RedirectToAction("Index");
    }
  }
}
