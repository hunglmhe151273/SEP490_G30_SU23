using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VBookHaven.Models.DTO
{
    public class StaffDTO
    {
        public int StaffId { get; set; }
        public string? FullName { get; set; }
        public decimal? StaffRevenue { get; set; }
    }
}
