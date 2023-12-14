using ISD_Project.Server.Models;

namespace ISD_Project.Server;

public class InsuranceContractDto
{
    public int CustomerId { get; set; }
    public int InsuranceId { get; set; }
    public ProfileStatus ProfileStatus { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
}
