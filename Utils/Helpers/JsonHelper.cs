using Newtonsoft.Json;
using System;
using System.Diagnostics;

namespace Utils.Helpers
{
    public static class JsonHelper
    {
        public static string JsonSerilize(object obj)
        {
            try
            {
                return JsonConvert.SerializeObject(obj);
            }
            catch (Exception e)
            {
                Debug.WriteLine($"ERROR: JsonSerilize\nMessage: {e.Message}");
                return null;
            }
        }

        public static T JsonDeserilize<T>(string str)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(str);
            }
            catch (Exception e)
            {
                Debug.WriteLine($"ERROR: JsonDeserilize\nMessage: {e.Message}");
                return default(T);
            }
        }

        public static T JsonDeserilizeByFilePath<T>(string path)
        {
            try
            {
                string str = FileHelper.ReadFileText(path);
                return JsonConvert.DeserializeObject<T>(str);
            }
            catch (Exception e)
            {
                Debug.WriteLine($"ERROR: JsonDeserilize\nMessage: {e.Message}");
                return default(T);
            }
        }
    }
}
