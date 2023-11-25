namespace ISD_Project.Server.Models.DTOs
{
    public class UserLoginResponse
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public List<string> Role { get; set; }
    }
}
