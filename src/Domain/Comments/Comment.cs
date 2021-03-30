using System;

namespace Domain.Comments
{
  /// <summary>
  /// Class Comment
  /// </summary>
  public class Comment
  {
    /// <summary>
    /// Id
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Message
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// Created
    /// </summary>
    public DateTime Created { get; set; }
  }
}
