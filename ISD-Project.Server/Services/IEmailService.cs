namespace ISD_Project.Server;

public interface IEmailService
{
    Task SendEmailAsync(string to, string subject, string body);

}
