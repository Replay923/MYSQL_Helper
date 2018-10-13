using System;
using System.Collections.Generic;
using System.Text;

namespace Utils.Helpers
{
    public class TimesHelper
    {
        private static readonly long epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).Ticks;
        /// <summary>
        /// 客户端时间
        /// </summary>
        /// <returns></returns>
        public static long ClientNow()
        {
            return (DateTime.UtcNow.Ticks - epoch) / 10000;
        }

        public static long ClientNowSeconds()
        {
            return (DateTime.UtcNow.Ticks - epoch) / 10000000;
        }

        /// <summary>
        /// 登陆前是客户端时间,登陆后是同步过的服务器时间
        /// </summary>
        /// <returns></returns>
        public static long Now()
        {
            return ClientNow();
        }

        /// <summary>
        /// 通过字符串解析时间，时间字符串以‘:’分割
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static TimeModel ParseTime(string str)
        {
            var t = str.Split(':');
            int h = 0, m = 0;
            int.TryParse(t[0], out h);
            int.TryParse(t[1], out m);
            return new TimeModel(h, m);
        }
    }
    public struct TimeModel
    {
        public int Hour;
        public int Minute;
        public TimeModel(int h, int m)
        {
            this.Hour = h;
            this.Minute = m;
        }
    }
}
