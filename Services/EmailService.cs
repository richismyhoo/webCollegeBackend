using System.Net;
using System.Net.Mail;

namespace webCollege.Services;

public class EmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendVerificationCodeAsync(string email, string code)
    {
        var smtpHost = _configuration["EmailSettings:SmtpHost"];
        var smtpPort = int.Parse(_configuration["EmailSettings:SmtpPort"]);
        var smtpUser = _configuration["EmailSettings:SmtpUser"];
        var smtpPassword = _configuration["EmailSettings:SmtpPassword"];
        var fromEmail = _configuration["EmailSettings:FromEmail"];

        using (var client = new SmtpClient(smtpHost, smtpPort))
        {
            client.Credentials = new NetworkCredential(smtpUser, smtpPassword);
            client.EnableSsl = true;

            var mailMessage = new MailMessage
            {
                From = new MailAddress(fromEmail),
                Subject = "Подтверждение почты",
                Body =
                    $"Ваш код подтверждения: {code} \n Если вы не регистрировались на сервисе, проигнорируйте это сообщение \n Не сообщайте данный код никому!",
                IsBodyHtml = false
            };
            
            mailMessage.To.Add(email);

            await client.SendMailAsync(mailMessage);
        }
    }
}