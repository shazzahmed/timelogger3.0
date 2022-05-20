using System;
using System.Collections.Generic;
using System.Text;

namespace TimeloggerCore.Common.Models
{
    public class ProgressBarModel
    {
        public string ProjectTitle { get; set; }
        public string Color { get; set; }
        public decimal Percentage { get; set; }
        public long TimeLog { get; set; }
        public int Time { get; set; }
    }
}
