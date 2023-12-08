using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ISD_Project.Server.Models.DTOs
{
    public class InsuranceDto
    {
        public int InsuranceId { get; set; }
        public string InsuranceName { get; set; } = string.Empty;
        public int CoveragePeriodInYears { get; set; } // thời gian hiệu lực của bảo hiểm
        public string SummaryDescription { get; set; } = string.Empty;
        public string DetailDescription { get; set; } = string.Empty;
        public decimal PriceAmount { get; set; }

        // 1 Insurance thuộc về 1 InsuranceType
        public int? InsuranceTypeId { get; set; }
        public InsuranceType? InsuranceType { get; set; }
    }
}
