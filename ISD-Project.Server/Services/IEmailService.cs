namespace ISD_Project.Server;

public interface IEmailService 
{
    Task SendEmail(string to, string subject, string body);
    
}
