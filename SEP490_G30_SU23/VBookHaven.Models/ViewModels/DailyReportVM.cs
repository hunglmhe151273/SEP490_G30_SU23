using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VBookHaven.Models.ViewModels
{
    public class DailyReportVM
    {
        public int Revenue { get; set; }
        public int NewOrder { get; set; }
        public int DoneOrder { get; set; }
        public int CancelledOrder { get; set; }

    }
}
