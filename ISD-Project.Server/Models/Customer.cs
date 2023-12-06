namespace ISD_Project.Server.Models
{
    public class Customer : User
    {
        public int IsApproved { get; set; } = (int)ProfileStatus.Pending;
        public HealthInformation? HealthInformation { get; set; }
    }

    public enum ProfileStatus
    {
        Pending,
        Approved,
        Rejected,
    }
}
