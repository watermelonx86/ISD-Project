namespace ISD_Project.Server.Models.DTOs
{
    public class CustomerDto : UserDto
    {
        public int IsApproved { get; set; } = (int)ProfileStatus.Pending;
        public string Nationality { get; set; } = String.Empty;
        public string Job { get; set; } = String.Empty;
    }
}
