namespace ISD_Project.Server.Models
{
    public class Customer : User
    {
        public int IsApproved { get; set; } = (int)ProfileStatus.Pending;
        public string Nationality { get; set; } = String.Empty;
        public string Job { get; set; } = String.Empty;
        public int? HealthInformationId { get; set; }
        public HealthInformation? HealthInformation { get; set; }
    }
}
