using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VBookHaven.Models.ViewModels
{
    public class ShippingInfoVM
    {
        public int CustomerId { get; set; }
        public bool IsDefault { get; set; }
        public ShippingInfo ShippingInfo { get; set; }
    }
}
