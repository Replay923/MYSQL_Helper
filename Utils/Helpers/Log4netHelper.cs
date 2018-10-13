using log4net;
using log4net.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace Utils.Helpers
{
    /// <summary>
    /// 日志等级
    /// </summary>
    public enum LogLevel
    {
        Error,
        Debug,
        Warning,
        Info
    }
    /// <summary>
    /// 单例模式初始化
    /// </summary>
    public class Log4Singleton
    {
        private ILog Log;
        private static Log4Singleton instance;
        private Log4Singleton() { }
        public static Log4Singleton getInstance()
        {
            if (instance == null)
            {
                instance = new Log4Singleton();
            }
            return instance;
        }
        /// <summary>
        /// 获取日志初始化器
        /// </summary>
        /// <returns></returns>
        public ILog Init(string logName)
        {
            Log = LogManager.GetLogger(Log4netHelper.Repository.Name, logName);
            return Log;
        }
    }
    /// <summary>
    /// 日志操作类
    /// </summary>
    public class Log4netHelper
    {
        /// <summary>
        /// log4net 仓储
        /// </summary>
        public static ILoggerRepository Repository { get; set; }
        /// <summary>
        /// 输出Erro日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ps"></param>
        public static void ErrorFormat(string message, params object[] ps)
        {
            message = string.Format(message, ps);
            StackTrace trace = new StackTrace();
            //获取是哪个类来调用的  
            var className = trace.GetFrame(1).GetMethod().DeclaringType;
            //获取方法名称
            MethodBase method = trace.GetFrame(1).GetMethod();
            var type = "类名：" + className.Namespace + "\n方法名：" + method.Name + "\n";
            WriteLog(LogLevel.Error, type + message);
        }
        /// <summary>
        /// 输出Erro日志
        /// </summary>
        /// <param name="message">日志内容</param>
        public static void Error(string message)
        {
            StackTrace trace = new StackTrace();
            //获取是哪个类来调用的  
            var className = trace.GetFrame(1).GetMethod().DeclaringType;
            //获取方法名称
            MethodBase method = trace.GetFrame(1).GetMethod();
            var type = "类名：" + className.Namespace + "\n方法名：" + method.Name + "\n";
            WriteLog(LogLevel.Error, type + message);
        }
        /// <summary>
        /// 输出Warning日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ps"></param>
        public static void WarningFormat(string message, params object[] ps)
        {
            message = string.Format(message, ps);
            StackTrace trace = new StackTrace();
            //获取是哪个类来调用的  
            var className = trace.GetFrame(1).GetMethod().DeclaringType;
            //获取方法名称
            MethodBase method = trace.GetFrame(1).GetMethod();
            var type = "类名：" + className.Namespace + "\n方法名：" + method.Name + "\n";
            //记录日志
            WriteLog(LogLevel.Warning, type + message);
        }
        /// <summary>
        /// 输出Warning日志
        /// </summary>
        /// <param name="message">日志内容</param>
        public static void Warning(string message)
        {
            StackTrace trace = new StackTrace();
            //获取是哪个类来调用的  
            var className = trace.GetFrame(1).GetMethod().DeclaringType;
            //获取方法名称
            MethodBase method = trace.GetFrame(1).GetMethod();
            var type = "类名：" + className.Namespace + "\n方法名：" + method.Name + "\n";
            //记录日志
            WriteLog(LogLevel.Warning, type + message);
        }
        /// <summary>
        /// 输出日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ps"></param>
        public static void InfoFormat(string message, params object[] ps)
        {
            message = string.Format(message, ps);
            StackTrace trace = new StackTrace();
            //获取是哪个类来调用的  
            var className = trace.GetFrame(1).GetMethod().DeclaringType;
            //获取方法名称
            MethodBase method = trace.GetFrame(1).GetMethod();
            var type = "类名：" + className.Namespace + "\n方法名：" + method.Name + "\n";
            //记录日志
            WriteLog(LogLevel.Info, type + message);
        }
        /// <summary>
        /// 输出Info日志
        /// </summary>
        /// <param name="message">日志内容</param>
        public static void Info(string message)
        {
            StackTrace trace = new StackTrace();
            //获取是哪个类来调用的  
            var className = trace.GetFrame(1).GetMethod().DeclaringType;
            //获取方法名称
            MethodBase method = trace.GetFrame(1).GetMethod();
            var type = "类名：" + className.Namespace + "\n方法名：" + method.Name + "\n";
            //记录日志
            WriteLog(LogLevel.Info, type + message);
        }
        /// <summary>
        /// 输出Debug日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ps"></param>
        public static void DebugFormat(string message, params object[] ps)
        {
            message = string.Format(message, ps);
            StackTrace trace = new StackTrace();
            //获取是哪个类来调用的  
            var className = trace.GetFrame(1).GetMethod().DeclaringType;
            //获取方法名称
            MethodBase method = trace.GetFrame(1).GetMethod();
            var type = "类名：" + className.Namespace + "\n方法名：" + method.Name + "\n";
            //记录日志
            WriteLog(LogLevel.Debug, type + message);
        }
        /// <summary>
        /// 输出Debug日志
        /// </summary>
        /// <param name="message">日志内容</param>
        public static void Debug(string message)
        {
            StackTrace trace = new StackTrace();
            //获取是哪个类来调用的  
            var className = trace.GetFrame(1).GetMethod().DeclaringType;
            //获取方法名称
            MethodBase method = trace.GetFrame(1).GetMethod();
            var type = "类名：" + className.Namespace + "\n方法名：" + method.Name + "\n";
            //记录日志
            WriteLog(LogLevel.Debug, type + message);
        }
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="logLevel">日志等级</param>
        /// <param name="message">日志信息</param>
        /// <param name="type">类名 方法名</param>
        private static void WriteLog(LogLevel logLevel, string message)
        {
            ILog Log = Log4Singleton.getInstance().Init(logLevel.ToString());
            switch (logLevel)
            {
                case LogLevel.Debug:
                    Log.Debug(message);
                    break;
                case LogLevel.Error:
                    Log.Error(message);
                    break;
                case LogLevel.Info:
                    Log.Info(message);
                    break;
                case LogLevel.Warning:
                    Log.Warn(message);
                    break;
            }

        }
    }
}
