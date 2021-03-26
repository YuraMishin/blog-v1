using System;

namespace Domain
{
  /// <summary>
  /// Class Post
  /// </summary>
  public class Post
  {
    /// <summary>
    /// Title
    /// </summary>
    public string Title { get; set; } = "";

    /// <summary>
    /// Body
    /// </summary>
    public string Body { get; set; } = "";

    /// <summary>
    /// Created
    /// </summary>
    public DateTime Created { get; set; } = DateTime.Now;
  }
}
