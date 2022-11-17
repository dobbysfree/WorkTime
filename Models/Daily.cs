using System;

namespace works.Models
{
    public class Daily
    {
        public int idx { get; set; }
        public int emp_num { get; set; }
        public string emp_name { get; set; }
        public DateTime base_date { get; set; }
        public DateTime start_dttm { get; set; }
        public DateTime end_dttm { get; set; }
        public int type_no { get; set; }
        public string type_nm { get; set; }
        public int type_category { get; set; }  // 0:근무, 1:휴가, 2:공휴일
        public decimal deduction { get; set; }
        public string text { get; set; }

        public TimeSpan worked_diff { get; set; }
        public double diff { get; set; }
        
    }
}