using Microsoft.AspNetCore.Http;

namespace Web.ViewModels
{
  /// <summary>
  /// Class PostViewModel
  /// </summary>
  public class PostViewModel
  {
    /// <summary>
    /// Id
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Title
    /// </summary>
    public string Title { get; set; } = "";

    /// <summary>
    /// Body
    /// </summary>
    public string Body { get; set; } = "";

    /// <summary>
    /// Image
    /// </summary>
    public IFormFile Image { get; set; } = null;
  }
}
