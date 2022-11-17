using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace works.Helpers
{
    public class Tool
    {
        #region 연차일수 구하기
        public static int Vacation(DateTime joinDt)
        {
            DateTime zeroDt = new DateTime(1, 1, 1);
            DateTime today  = DateTime.Today;

            TimeSpan ts     = today - joinDt;
            int aftYear     = (zeroDt + ts).Year - 1;
            int aftMonth    = Convert.ToInt32(ts.Days / 30.4368499);

            // 1년 미만
            int cnt = 0;
            if (aftYear < 1)
                cnt = aftMonth;
            else
                cnt = 15 + ((aftYear - 1) / 2);
            
            return cnt;
        }
        #endregion

        #region 근무시간 구하기
        public static TimeSpan WorkHours(DateTime start, DateTime end)
        {
            if (start == new DateTime() || end == new DateTime())
                return TimeSpan.Zero;

            TimeSpan diff   = end - start;
            int deduct      = (diff.Hours / 4) * 30;
            return diff - TimeSpan.FromMinutes(deduct);
        }
        #endregion

        #region 암호화
        public static string Encrypt(string password)
        {
            var provider = MD5.Create();
            string salt = "S0m3R@nd0mSalt";
            byte[] bytes = provider.ComputeHash(Encoding.UTF32.GetBytes(salt + password));
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }
        #endregion

        #region 평일 일수 구하기
        public static int BusinessDays()
        {
            DateTime now = DateTime.Now;
            DateTime first = new DateTime(now.Year, now.Month, 1).AddDays(-1);
            DateTime end = new DateTime(now.Year, now.Month, DateTime.DaysInMonth(now.Year, now.Month));

            var cnt = 0;

            while (true)
            {
                first = first.Date.AddDays(1);

                if (first.Date > end.Date)
                    break;

                if (first.DayOfWeek == DayOfWeek.Saturday || first.DayOfWeek == DayOfWeek.Sunday)
                    continue;

                cnt++;
            }

            return cnt;
        }
        #endregion
    }
}