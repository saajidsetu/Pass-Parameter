using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PassParameter.Models
{
    public class ClsStudentinfo
    {
        public string Year { get; set; }
        public int WeekID { get; set; }
        public string WeekNo { get; set; }
        public string ParaNo { get; set; }

    }

    public class ClsStudentupdate
    {
        public string Studentid { get; set; }
        public int WeekID { get; set; }
        public string WeekNo { get; set; }
        public string ParaNo { get; set; }
        public string Year { get; set; }

    }
}