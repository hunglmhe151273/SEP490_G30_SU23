using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VBookHaven.Models.ViewModels
{
	public class PurchaseOrderVM
	{
		public PurchaseOrderVM() {
			ProductIdList = new List<int>();
			QuantityList = new List<int>();
			PriceList = new List<int>();
            PurchaseDiscountList = new List<double>();
		}
		public int? SupplierID { get; set; }
		public string? Note { get; set; }
		public List<int> ProductIdList { get; set; }
		public List<int> QuantityList { get; set; }
		public List<int> PriceList { get; set; }
		public List<double> PurchaseDiscountList { get; set; }
        public double AmountPaid { get; set; }
        public double VAT { get; set; }
	}
}
