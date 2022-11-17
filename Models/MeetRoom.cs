using System;

namespace works.Models
{
    public class MeetRoom
    {
        public int idx { get; set; }
        public int emp_num { get; set; }
        public string emp_name { get; set; }
        public int room_num { get; set; }
        public DateTime start_dttm { get; set; }
        public DateTime end_dttm { get; set; }
        public string note { get; set; }
        public DateTime chg_dttm { get; set; }
        public string text { get; set; }
        
        public int reflag { get; set; }
    }
}