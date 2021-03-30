using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
  /// <summary>
  /// Class CommentViewModel
  /// </summary>
  public class CommentViewModel
  {
    /// <summary>
    /// PostId
    /// </summary>
    [Required]
    public int PostId { get; set; }

    /// <summary>
    /// MainCommentId
    /// </summary>
    [Required]
    public int MainCommentId { get; set; }

    /// <summary>
    /// Message
    /// </summary>
    [Required]
    public string Message { get; set; }
  }
}
