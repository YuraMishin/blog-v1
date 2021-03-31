using System.Collections.Generic;
using Domain;

namespace Application.ViewModels
{
  /// <summary>
  /// Class IndexViewModel
  /// </summary>
  public class IndexViewModel
  {
    /// <summary>
    /// PageNumber
    /// </summary>
    public int PageNumber { get; set; }

    /// <summary>
    /// PageCount
    /// </summary>
    public int PageCount { get; set; }

    /// <summary>
    /// NextPage
    /// </summary>
    public bool NextPage { get; set; }

    /// <summary>
    /// Category
    /// </summary>
    public string Category { get; set; }

    /// <summary>
    /// Posts
    /// </summary>
    public IEnumerable<Post> Posts { get; set; }

    /// <summary>
    /// Pages
    /// </summary>
    public IEnumerable<int> Pages { get; internal set; }

    /// <summary>
    /// Search
    /// </summary>
    public string Search { get; set; }
  }
}
