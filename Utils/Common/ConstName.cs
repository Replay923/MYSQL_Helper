using System;
using System.Collections.Generic;
using System.Text;

namespace Utils.Common
{
    public class ConstName
    {
        public static string AppKey => "appKey";

        public static string AppName => "appName";

        public static string AppConfig => "appConfig";

        public static string XmlFileName => "xmlFileName";

        public static string countryName => "countryName";

        public static string countryCode => "countryCode";

        public static string BeiJingTimeZoneId => "China Standard Time";

#if DEBUG
        public static string Linux_ShangHaiTimeZoneId => "China Standard Time";
#else
        public static string Linux_ShangHaiTimeZoneId => "Asia/Shanghai";
#endif
        public static string ServerUrlName => "DataServerUrl";

        public static string DeveloperPostApi => "http://127.0.0.1:5001/ReplayConector/dev";
    }
}
