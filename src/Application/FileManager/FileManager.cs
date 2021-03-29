using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using PhotoSauce.MagicScaler;

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
        MagicImageProcessor.ProcessImage(image.OpenReadStream(), fileStream, ImageOptions());
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

    /// <inheritdoc />
    public bool RemoveImage(string image)
    {
      try
      {
        var file = Path.Combine(_imagePath, image);
        if (File.Exists(file))
          File.Delete(file);
        return true;
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
        return false;
      }
    }

    /// <summary>
    /// Method processes image via Magic Scaler
    /// </summary>
    /// <returns></returns>
    private ProcessImageSettings ImageOptions() => new ProcessImageSettings
    {
      Width = 800,
      Height = 500,
      ResizeMode = CropScaleMode.Crop,
      SaveFormat = FileFormat.Jpeg,
      JpegQuality = 100,
      JpegSubsampleMode = ChromaSubsampleMode.Subsample420
    };
  }
}
