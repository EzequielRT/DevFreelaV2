namespace DevFreela.Infra.Email;

public interface IEmailService
{
    Task SendEmailAsync(string to, string subject, string body);
}