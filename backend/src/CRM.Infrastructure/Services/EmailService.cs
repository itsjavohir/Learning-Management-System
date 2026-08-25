using System.Net;
using System.Net.Mail;
using CRM.Application.Common.Settings;
using CRM.Application.Interfaces.Services;
using Microsoft.Extensions.Options;

namespace CRM.Infrastructure.Services;

public class EmailService(IOptions<EmailSettings> settings) : IEmailService
{
    private readonly EmailSettings _settings = settings.Value;

    public async Task SendWelcomeAsync(string toEmail, string fullName, string tempPassword, CancellationToken cancellationToken)
    {
        var subject = "Добро пожаловать в CRM";
        var body = $"""
                    <p>Здравствуйте, <strong>{fullName}</strong>!</p>
                    <p>Ваш аккаунт создан.</p>
                    <p>Email: <strong>{toEmail}</strong></p>
                    <p>Временный пароль: <strong>{tempPassword}</strong></p>
                    <p>Пожалуйста, смените пароль после первого входа.</p>
                    """;

        await SendAsync(toEmail, subject, body, cancellationToken);
    }

    public async Task SendAsync(string toEmail, string subject, string body, CancellationToken cancellationToken)
    {
        using var client = new SmtpClient(_settings.Host, _settings.Port)
        {
            Credentials = new NetworkCredential(_settings.UserName, _settings.Password),
            EnableSsl = true,
            Timeout = 5000
        };

        var message = new MailMessage
        {
            From = new MailAddress(_settings.FromEmail, _settings.FromName),
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };

        message.To.Add(toEmail);
        await client.SendMailAsync(message, cancellationToken);
    }
}