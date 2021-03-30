using System.Threading.Tasks;

namespace Web.Services.Email
{
  /// <summary>
  /// Interface IEmailService
  /// </summary>
  public interface IEmailService
  {
    /// <summary>
    /// Method sends an email
    /// </summary>
    /// <param name="email">email</param>
    /// <param name="subject">subject</param>
    /// <param name="message">message</param>
    /// <returns>Task</returns>
    Task SendEmail(string email, string subject, string message);
  }
}
