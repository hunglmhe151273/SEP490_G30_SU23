﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VBookHaven.Models.ViewModels
{
    public class DailyReportVM
    {
        public decimal Revenue { get; set; }
        public int ProcessOrder { get; set; }
        public int DoneOrder { get; set; }
        public int CancelledOrder { get; set; }

    }
}
