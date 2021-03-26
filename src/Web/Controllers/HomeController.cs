using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
  /// <summary>
  /// Сlass HomeController
  /// </summary>
  public class HomeController : Controller
  {
    /// <summary>
    /// Method displays index UI
    /// </summary>
    /// <returns></returns>
    public IActionResult Index()
    {
      return View();
    }

    /// <summary>
    /// Method displays post UI
    /// </summary>
    /// <returns></returns>
    public IActionResult Post()
    {
      return View();
    }
  }
}
