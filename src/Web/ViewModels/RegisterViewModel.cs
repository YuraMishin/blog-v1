using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
  /// <summary>
  /// Class RegisterViewModel
  /// </summary>
  public class RegisterViewModel
  {
    /// <summary>
    /// Email
    /// </summary>
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    /// <summary>
    /// Password
    /// </summary>
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    /// <summary>
    /// ConfirmPassword
    /// </summary>
    [Required]
    [DataType(DataType.Password)]
    [Compare("Password")]
    public string ConfirmPassword { get; set; }
  }
}
