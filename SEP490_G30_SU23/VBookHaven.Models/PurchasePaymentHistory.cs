using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VBookHaven.Models
{
    public partial class PurchasePaymentHistory
    {
        public int PaymentId { get; set; }

        public DateTime? PaymentDate { get; set; }

        public decimal? PaymentAmount { get; set; }

        public int? PurchaseId { get; set; }

        public virtual PurchaseOrder? Purchase { get; set; }
        public int? StaffId { get; set; }

        public virtual Staff? Staff { get; set; }
    }
}
