using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VBookHaven.Models.ViewModels
{
    public class ShippingInfoVM
    {
        [ValidateNever]
        public bool shippingInfosIsNull { get; set; }
        public int CustomerId { get; set; }
        public bool IsDefault { get; set; }
        public ShippingInfo ShippingInfo { get; set; }
        [ValidateNever]
        public List<ShippingInfo> ShippingInfos { get; set; }
    }
}
