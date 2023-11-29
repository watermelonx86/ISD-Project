namespace ISD_Project.Server.Models.DTOs
{
    public class UserLoginResponse
    {
        public int UserAccountId { get; set; }
        public string? Token { get; set; } = String.Empty;
        public List<string> Role { get; set; } = new List<string>();
        public int IsActivated { get; set; }
        public int UserId { get; set; }
    }
}
