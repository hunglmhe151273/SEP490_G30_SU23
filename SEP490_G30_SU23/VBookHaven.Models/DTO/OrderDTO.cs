using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VBookHaven.Models.DTO
{
    public class OrderDTO
    {
        public int OrderId { get; set; }

        public DateTime? OrderDate { get; set; }

        public string? Status { get; set; }

        public int? CustomerId { get; set; }

        public string? CustomerName { get; set; }

        public string? ShipAddress { get; set; }

        public string? Phone { get; set; }

        public string? Note { get; set; }

        public int? AmountPaid { get; set; }

        public double? VAT { get; set; }
        //add to view
        public decimal? ToTalPayment { get; set; }
        public string? StaffName { get; set; }

    }
}
