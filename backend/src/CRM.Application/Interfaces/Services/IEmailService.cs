namespace CRM.Application.Interfaces.Services;

public interface IEmailService
{
     Task SendWelcomeAsync (string toEmail, string fullName , string tempPassword,CancellationToken cancellationToken);
     Task SendPasswordResetCodeAsync(string toEmail, string fullName, string code, CancellationToken cancellationToken);
     Task SendAsync (string toEmail, string subject , string body ,CancellationToken cancellationToken);
}
