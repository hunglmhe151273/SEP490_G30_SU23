using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VBookHaven.Models.DTO
{
    public class ProductDTO
    {
		public int ProductId { get; set; }

		public string? Name { get; set; }

		public string? Barcode { get; set; }

		public string? Unit { get; set; }

		public int? UnitInStock { get; set; }

		public int? PurchasePrice { get; set; }

		public int? RetailPrice { get; set; }

		public double? RetailDiscount { get; set; }

		public int? WholesalePrice { get; set; }

		public double? WholesaleDiscount { get; set; }

		public string? Size { get; set; }

		public string? Weight { get; set; }

		public string? Description { get; set; }

		public bool? Status { get; set; }

		public bool? IsBook { get; set; }

		public int? SubCategoryId { get; set; }
	}
}
