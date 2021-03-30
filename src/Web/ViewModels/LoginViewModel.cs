using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
  /// <summary>
  /// Class LoginViewModel
  /// </summary>
  public class LoginViewModel
  {
    /// <summary>
    /// UserName
    /// </summary>
    [Required]
    public string UserName { get; set; }

    /// <summary>
    /// Password
    /// </summary>
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
  }
}
