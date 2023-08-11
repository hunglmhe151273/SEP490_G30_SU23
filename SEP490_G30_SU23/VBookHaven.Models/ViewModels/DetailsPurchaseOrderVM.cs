using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VBookHaven.ViewModels;

namespace VBookHaven.Models.ViewModels
{
    public class DetailsPurchaseOrderVM
    {
        public DetailsPurchaseOrderVM()
        {
            pO = new PurchaseOrder();
            PPHistory = new PurchasePaymentHistory();
        }

        public PurchaseOrder pO { get; set; }
        public decimal TotalPayment { get; set; }
        public decimal? Paid { get; set; }
        public decimal? Unpaid { get; set; }
        public PurchasePaymentHistory PPHistory { get; set; }
    }
}
