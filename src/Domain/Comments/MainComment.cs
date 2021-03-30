using System.Collections.Generic;

namespace Domain.Comments
{
  /// <summary>
  /// Class MainComment
  /// </summary>
  public class MainComment : Comment
  {
    /// <summary>
    /// SubComments
    /// </summary>
    public List<SubComment> SubComments { get; set; }
  }
}
