using System;
using System.Collections.Generic;
using System.Text;
using Utils.Common;

namespace Utils.Ex
{
    public static class DateTimeEx
    {
        /// <summary>
        /// 给定时区id，获取时区信息
        /// </summary>
        /// <param name="tzID"></param>
        /// <returns></returns>
        public static TimeZoneInfo GetGivenTimeZoneInfo(string tzID)
        {
            TimeZoneInfo.ClearCachedData();
            return TimeZoneInfo.FindSystemTimeZoneById(tzID);
        }
        /// <summary>
        /// DateTime 扩展：给定时区id，获取时区信息
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="tzID"></param>
        /// <returns></returns>
        public static TimeZoneInfo GetGivenTimeZoneInfo(this DateTime dateTime, string tzID)
        {
            TimeZoneInfo.ClearCachedData();
            return TimeZoneInfo.FindSystemTimeZoneById(tzID);
        }
        /// <summary>
        /// 服务器本地时间转换为北京时间（CST）
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime LoaclConvertToCST(this DateTime dateTime)
        {
            TimeZoneInfo.ClearCachedData();
            dateTime = DateTime.Now;
            TimeZoneInfo timeZoneSource = TimeZoneInfo.Local;
            TimeZoneInfo timeZoneDestination = TimeZoneInfo.FindSystemTimeZoneById(ConstName.Linux_ShangHaiTimeZoneId);
            return TimeZoneInfo.ConvertTime(dateTime, timeZoneSource, timeZoneDestination);
        }
        /// <summary>
        /// 转化UTC时间为CST(北京时间)
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime ConvertToCST(this DateTime dateTime)
        {
            TimeZoneInfo.ClearCachedData();
            return System.TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dateTime, ConstName.Linux_ShangHaiTimeZoneId);
        }

        /// <summary>
        /// 转化CST(北京)时间为GMT（也就是UTC时间）
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime CSTConvertToGMT(this DateTime dateTime)
        {
            TimeZoneInfo.ClearCachedData();
            TimeZoneInfo timeZoneSource = TimeZoneInfo.FindSystemTimeZoneById(ConstName.Linux_ShangHaiTimeZoneId);
            TimeZoneInfo timeZoneDestination = TimeZoneInfo.FindSystemTimeZoneById("UTC");
            return TimeZoneInfo.ConvertTime(dateTime, timeZoneSource, timeZoneDestination);
        }

        /// <summary>
        /// 转化CST(北京)时间为UTC时间
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime CSTConvertToUTC(this DateTime dateTime)
        {
            TimeZoneInfo.ClearCachedData();
            return TimeZoneInfo.ConvertTimeToUtc(dateTime, TimeZoneInfo.FindSystemTimeZoneById(ConstName.Linux_ShangHaiTimeZoneId));
        }
    }
}
