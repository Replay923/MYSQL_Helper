using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Utils.Helpers
{
    public static class FileHelper
    {
        public static string ReadFileText(string path)
        {
            return File.ReadAllText(path);
        }
        /// <summary>
        /// 反射获取类信息
        /// </summary>
        /// <param name="assemblyNamePath">程序集路径</param>
        /// <param name="className">类名</param>
        /// <returns></returns>
        public static Type GetAbsolutePath(string assemblyNamePath, string className)
        {
            Assembly assembly = Assembly.LoadFile(assemblyNamePath);
            Type type = assembly.GetType(className);
            return type;
        }
        /// <summary>
        /// 反射创建类单例。用来创建JobLisener
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assemblyNamePath"></param>
        /// <param name="className"></param>
        /// <returns></returns>
        public static T CreateInstance<T>(string assemblyNamePath, string className)
        {
            Assembly assembly = Assembly.LoadFile(assemblyNamePath);
            return (T)assembly.CreateInstance(className);
        }

        public static void GetAllFiles(List<string> files, string dir)
        {
            string[] fls = Directory.GetFiles(dir);
            foreach (string fl in fls)
            {
                files.Add(fl);
            }

            string[] subDirs = Directory.GetDirectories(dir);
            foreach (string subDir in subDirs)
            {
                GetAllFiles(files, subDir);
            }
        }

        public static void CleanDirectory(string dir)
        {
            foreach (string subdir in Directory.GetDirectories(dir))
            {
                Directory.Delete(subdir, true);
            }

            foreach (string subFile in Directory.GetFiles(dir))
            {
                File.Delete(subFile);
            }
        }

        public static void CopyDirectory(string srcDir, string tgtDir)
        {
            DirectoryInfo source = new DirectoryInfo(srcDir);
            DirectoryInfo target = new DirectoryInfo(tgtDir);

            if (target.FullName.StartsWith(source.FullName, StringComparison.CurrentCultureIgnoreCase))
            {
                throw new Exception("父目录不能拷贝到子目录！");
            }

            if (!source.Exists)
            {
                return;
            }

            if (!target.Exists)
            {
                target.Create();
            }

            FileInfo[] files = source.GetFiles();

            for (int i = 0; i < files.Length; i++)
            {
                File.Copy(files[i].FullName, Path.Combine(target.FullName, files[i].Name), true);
            }

            DirectoryInfo[] dirs = source.GetDirectories();

            for (int j = 0; j < dirs.Length; j++)
            {
                CopyDirectory(dirs[j].FullName, Path.Combine(target.FullName, dirs[j].Name));
            }
        }
    }
}
