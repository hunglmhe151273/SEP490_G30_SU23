using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VBookHaven.Models.ViewModels
{
    public class CreatePurchaseOrderVM
    {
        public CreatePurchaseOrderVM()
        {
            ProductIdList = new List<int>();
            QuantityList = new List<int>();
            UnitPriceList = new List<int>();
            DiscountList = new List<double>();
            PurchaseOrderEdit = new PurchaseOrder();
        }
        public int SupplierID { get; set; }
        public string? Note { get; set; }
        public List<int> ProductIdList { get; set; }
        public List<int> QuantityList { get; set; }
        public List<int> UnitPriceList { get; set; }
        public List<double> DiscountList { get; set; }
        public decimal? AmountPaid { get; set; }
        public double? VAT { get; set; }
        public PurchaseOrder PurchaseOrderEdit { get; set; }
    }
}
