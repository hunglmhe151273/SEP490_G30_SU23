using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VBookHaven.Models.ViewModels
{
    public class Manage_ShippingInfoVM
    {
        public Manage_ShippingInfoVM()
        {
            ShippingInfo = new ShippingInfo();
            Customer = new Customer();
        }
        public ShippingInfo ShippingInfo { get; set; }
        public Customer Customer { get; set; }
    }
}
