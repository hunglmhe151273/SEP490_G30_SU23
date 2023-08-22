using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VBookHaven.Models.ViewModels
{
    public class SupplierDetailsVM
    {
        public SupplierDetailsVM()
        {
            Supplier = new Supplier();
            SupplierProducts = new List<Product>();
            foreach(var product in SupplierProducts)
            {
                product.Images = new List<Image>();
            }
        }
        public Supplier Supplier { get; set; }
        public List<Product> SupplierProducts { get; set; }
    }
}
