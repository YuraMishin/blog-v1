using Application.FileManager;
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
    public IActionResult Image(string image) =>
    new FileStreamResult(
      _fileManager.ImageStream(image),
      $"image/{image.Substring(image.LastIndexOf('.') + 1)}");

  }
}
