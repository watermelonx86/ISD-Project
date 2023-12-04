using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ISD_Project.Server.Models.DTOs
{
    public class InsuranceDto
    {
        public int InsuranceId { get; set; }
        public string InsuranceName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public float PriceAmount { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }


    }
}
