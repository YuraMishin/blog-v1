using System;
using System.Collections.Generic;
using Domain.Comments;

namespace Domain
{
  /// <summary>
  /// Class Post
  /// </summary>
  public class Post
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
    public string Image { get; set; } = "";

    /// <summary>
    /// Description
    /// </summary>
    public string Description { get; set; } = "";

    /// <summary>
    /// Tags
    /// </summary>
    public string Tags { get; set; } = "";

    /// <summary>
    /// Category
    /// </summary>
    public string Category { get; set; } = "";

    /// <summary>
    /// Created
    /// </summary>
    public DateTime Created { get; set; } = DateTime.Now;

    /// <summary>
    /// MainComments
    /// </summary>
    public List<MainComment> MainComments { get; set; }
  }
}
