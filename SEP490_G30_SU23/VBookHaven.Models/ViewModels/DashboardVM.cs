using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VBookHaven.Models.ViewModels
{
    public class DashboardVM
    {
        public int TotalCustomer { get; set; }
        public int TotalOrders { get; set; }
        public int TotalPurchaseOrders { get; set; }
        public int TotalStaff { get; set; }
        public int DefaultYear { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> YearList { get; set; }
    }
}
