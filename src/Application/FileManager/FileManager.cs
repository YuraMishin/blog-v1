using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Application.FileManager
{
  /// <summary>
  /// Class FileManager
  /// </summary>
  public class FileManager : IFileManager
  {
    private readonly string _imagePath;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="config">config</param>
    public FileManager(IConfiguration config)
    {
      _imagePath = config["Path:Images"];
    }

    /// <inheritdoc />
    public async Task<string> SaveImage(IFormFile image)
    {
      try
      {
        var savePath = Path.Combine(_imagePath);
        if (!Directory.Exists(savePath))
        {
          Directory.CreateDirectory(savePath);
        }

        var mime = image.FileName.Substring(image.FileName.LastIndexOf('.'));
        var fileName = $"img_{DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss")}{mime}";
        await using var fileStream = new FileStream(
          Path.Combine(savePath, fileName),
          FileMode.Create);
        await image.CopyToAsync(fileStream);
        return fileName;
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
        return "Error";
      }
    }

    /// <inheritdoc />
    public FileStream ImageStream(string image)
    {
      return new FileStream(
        Path.Combine(_imagePath, image),
        FileMode.Open,
        FileAccess.Read);
    }
  }
}
