using System;
using System.Collections.Generic;
using System.Text;

namespace Utils.Helpers
{
    public class PostResult
    {
        public string error;
        public PostResult(string mess = "Error")
        {
            error = mess;
        }
    }
    public enum OperateTable
    {
        PagePost,
        NewsItemPost,
        NewsDetailPost,
        MediaPost,
    }
    public enum OperateType
    {
        Insert,
        SelectAll,
        Select,
        Delete,
        Update
    }
    public class DBHelper
    {
        public static string Operate(OperateTable operationName, OperateType operate, object body)
        {
            OperateBody op = new OperateBody(operationName.ToString(), operate.ToString(), body);
            return JsonHelper.JsonSerilize(op);
        }

        public class OperateBody
        {
            public string operationName;
            public string operateType;
            public object body;

            public OperateBody(string name, string type, object b)
            {
                operationName = name;
                operateType = type;
                body = b;
            }
        }
    }
}
