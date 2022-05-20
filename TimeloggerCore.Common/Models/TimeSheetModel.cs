using System;
using System.Collections.Generic;
using System.Text;

namespace TimeloggerCore.Common.Models
{
    public class TimeSheetModel
    {
        public string UserID { get; set; }
        public List<string> myID { get; set; }
        public int ProjectID { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public List<string> UserIds { get; set; }
        public List<int> ProjectIds { get; set; }
    }
    public class TimeSheetReportModel
    {
        public string FullName { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public List<TimeLogModel> TimeLogs { get; set; }
    }
}
