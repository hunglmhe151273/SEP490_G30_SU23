using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VBookHaven.Models
{
	public partial class OrderPaymentHistory
	{
		public int PaymentId { get; set; }

		public DateTime? PaymentDate { get; set; }

		public decimal? PaymentAmount { get; set; }

		public string? PaymentMethod { get; set; }

		public int? OrderId { get; set; }

		public virtual Order? Order { get; set; }
		public int? StaffId { get; set; }

		public virtual Staff? Staff { get; set; }
	}
}
