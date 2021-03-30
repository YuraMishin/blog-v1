using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Web.Configuration;

namespace Web.Services.Email
{
  /// <summary>
  /// Class EmailService
  /// </summary>
  public class EmailService : IEmailService
  {
    private readonly SmtpSettings _settings;
    private readonly SmtpClient _client;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="options">options</param>
    public EmailService(IOptions<SmtpSettings> options)
    {
      _settings = options.Value;
      _client = new SmtpClient(_settings.Server, int.Parse(_settings.Port))
      {
        Credentials = new NetworkCredential(_settings.Username, _settings.Password),
      };
    }

    /// <inheritdoc />
    public Task SendEmail(string email, string subject, string message)
    {
      var mailMessage = new MailMessage(
          _settings.From,
          email,
          subject,
          message);

      return _client.SendMailAsync(mailMessage);
    }
  }
}
