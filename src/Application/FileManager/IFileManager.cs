using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Application.FileManager
{
  /// <summary>
  /// Interface IFileManager
  /// </summary>
  public interface IFileManager
  {
    /// <summary>
    /// Method saves the image
    /// </summary>
    /// <param name="image">image</param>
    /// <returns></returns>
    Task<string> SaveImage(IFormFile image);

    /// <summary>
    /// Method streams the image
    /// </summary>
    /// <param name="image">image</param>
    /// <returns></returns>
    FileStream ImageStream(string image);
  }
}
