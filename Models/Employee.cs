using System;
using System.Collections.Generic;

namespace works.Models
{
    public class Employee
    {
        public int num { get; set; }                // 사번
        public string name { get; set; }            // 이름
        public string contact { get; set; }         // 연락처
        public string email { get; set; }           // 이메일
        public string _email { get; set; }          // 임시 이메일
        public string team { get; set; }            // 소속 팀
        public DateTime join_date { get; set; }     // 입사일
        public int tot_annual { get; set; }         // 연차일수
        public decimal use_annual { get; set; }     // 사용연차
        public int auth { get; set; }               // 권한
        public string retire_date { get; set; }     // 퇴사일


        public int work_hour { get; set; }          // 근무해야할 시간
        public TimeSpan worked { get; set; }        // 근무한 시간
        public TimeSpan work_diff { get; set; }     // 미달/초과 시간

        public bool visible_details;

        public TimeSpan commute_time { get; set; }  // 출퇴근 총시간
        public TimeSpan remote_time { get; set; }   // 원격 총시간
        public TimeSpan outside_time { get; set; }  // 외근 총시간
        
        public List<Daily> dailies { get; set; }
        public Dictionary<DateTime, double> day_hour { get; set; }
        public string diff_color { get; set; }     // 미달 초과 for font color

        public Dictionary<DateTime, double> chart_works { get; set; }
    }
}