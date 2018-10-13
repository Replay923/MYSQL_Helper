using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using DataHelper.Attributes;

namespace DataHelper
{
    public static class ExSql
    {
        public static string WHERE(this string str, string condition)
        {
            if (string.IsNullOrEmpty(condition))
                return str;
            return string.Format("{0} WHERE {1}", str, condition);
        }
        public static string AS(this string str, string condition)
        {
            if (string.IsNullOrEmpty(condition))
                return str;
            return string.Format("{0} AS {1}", str, condition);
        }
        public static string ON(this string str, string condition)
        {
            if (string.IsNullOrEmpty(condition))
                return str;
            return string.Format("{0} ON {1}", str, condition);
        }
        /// <summary>
        /// inner join(内连接,或等值连接)：取得两个表中存在连接匹配关系的记录。
        /// </summary>
        public static string INNER_JOIN(this string str, string sql)
        {
            if (string.IsNullOrEmpty(sql))
                return str;
            return string.Format("{0} INNER JOIN({1})", str, sql);
        }
        /// <summary>
        /// left join(左连接)：取得左表（atable）完全记录，即是右表（btable）并无对应匹配记录。
        /// </summary>
        public static string LEFT_JOIN(this string str, string sql)
        {
            if (string.IsNullOrEmpty(sql))
                return str;
            return string.Format("{0} LEFT JOIN({1})", str, sql);
        }
        /// <summary>
        /// right join(右连接)：与 LEFT JOIN 相反，取得右表（btable）完全记录，即是左表（atable）并无匹配对应记录。
        /// </summary>
        public static string RIGHT_JOIN(this string str, string sql)
        {
            if (string.IsNullOrEmpty(sql))
                return str;
            return string.Format("{0} RIGHT JOIN({1})", str, sql);
        }
        public static string LIMIT(this string str, params int[] conditions)
        {
            if (conditions.Length < 1)
                return str;
            return string.Format("{0} LIMIT {1}", str, string.Join(',', conditions));
        }
        public static string LIMIT(this string str, params string[] conditions)
        {
            if (conditions.Length < 1)
                return str;
            return string.Format("{0} LIMIT {1}", str, string.Join(',', conditions));
        }
        public static string ORDER_BY(this string str, string condition)
        {
            if (string.IsNullOrEmpty(condition))
                return str;
            if (str.EndsWith("SC") || str.EndsWith("SC,"))
                return string.Format("{0},{1}", str, condition);
            return string.Format("{0} ORDER BY {1}", str, condition);
        }

        public static string AND(this string str, string condition)
        {
            if (string.IsNullOrEmpty(condition))
                return str;
            return string.Format("{0} AND {1}", str, condition);
        }

        public static string DESC(this string str)
        {
            return string.Format("{0} DESC", str);
        }
        public static string ASC(this string str)
        {
            return string.Format("{0} ASC", str);
        }
    }

    public class MySqlHelper
    {
        /// <summary>
        /// 单例构造函数
        /// </summary>
        private MySqlHelper() { }

        public static MySqlHelper _obj;

        public static MySqlHelper GetHelper()
        {
            if (_obj == null)
            {
                _obj = new MySqlHelper();
            }

            return _obj;
        }

        private StringBuilder _strBuilder = new StringBuilder();

        private const string IDENTITYSQL = "SELECT @@IDENTITY";

        private const string ADDTEMPLATE = "INSERT INTO {0} ({1}) VALUES ({2})";

        private const string SELECTTEMPLATE = "SELECT {0} FROM {1}{2}";

        private const string UPDATETEMPLATE = "UPDATE {0} SET {1} WHERE {2}=@{3}";

        private const string KEYWORDTEMPLATE = "`{0}`";

        /// <summary>
        /// 获得新增sql语句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public string InsertSql<T>()
        {
            var type = typeof(T);

            var props = type.GetProperties();

            var tableName = GetTableName<T>();

            var priName = GetPrimaryKeyName<T>();

            var fieldNameList = GetFieldNameList<T>();

            var idenNameList = GetIdentityFieldNameList<T>();

            //fieldNameList = fieldNameList.Where(x => !idenNameList.Contains(x)).ToList();

            // Get fieldNameList

            var columnStr = string.Join(",", fieldNameList.Select(x => string.Format(KEYWORDTEMPLATE, x)));

            var result = string.Format(ADDTEMPLATE, tableName, columnStr, string.Join(",", fieldNameList.Select(x => "@" + x)));

            return result;
        }

        /// <summary>
        /// 获得查询sql语句
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="condition">条件sql语句</param>
        /// <returns></returns>
        public string SelectSql<T>(string condition = null)
        {
            var tableName = GetTableName<T>();
            tableName = string.Format(KEYWORDTEMPLATE, tableName);
            var fieldNameList = GetFieldNameList<T>();

            condition = string.IsNullOrEmpty(condition) ? "" : " WHERE " + condition;

            var columnNames = string.Join(",", fieldNameList.Select(x => string.Format(KEYWORDTEMPLATE, x)));

            var result = string.Format(SELECTTEMPLATE, columnNames, tableName, condition);

            return result;
        }
        /// <summary>
        /// 查询语句创建，指定 表名注释为 AS t1
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="asName">表名标签</param>
        /// <param name="condition">条件语句</param>
        /// <returns></returns>
        public string SelectSql<T>(string asName, string condition = null)
        {
            var tableName = GetTableName<T>();
            tableName = string.Format(KEYWORDTEMPLATE, tableName).AS(asName);
            var fieldNameList = GetFieldNameList<T>();

            condition = string.IsNullOrEmpty(condition) ? "" : " WHERE " + condition;

            var columnNames = string.Join(",", fieldNameList.Select(x => string.Format("{0}.`{1}`", asName, x)));

            var result = string.Format(SELECTTEMPLATE, columnNames, tableName, condition);

            return result;
        }

        /// <summary>
        /// 获得删除sql
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="condition"></param>
        /// <returns></returns>
        public string DeleteSql<T>(string condition)
        {
            if (string.IsNullOrEmpty(condition))
            {
                throw new Exception("condition不能为空");
            }

            var tableName = GetTableName<T>();

            tableName = string.Format(KEYWORDTEMPLATE, tableName);

            return $"DELETE FROM {tableName} WHERE " + condition;
        }

        /// <summary>
        /// 获得修改sql
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="condition"></param>
        /// <returns></returns>
        public string UpdateSql<T>(string condition = null)
        {
            var tableName = GetTableName<T>();
            var priKeyName = GetPrimaryKeyName<T>();
            var fieldNames = GetFieldNameList<T>();
            var idenNames = GetIdentityFieldNameList<T>();

            fieldNames = fieldNames.Where(x => !idenNames.Contains(x) && string.Compare(x, priKeyName, true) != 0).ToList();

            var result = string.Format(UPDATETEMPLATE,
                tableName,
                string.Join(",", fieldNames.Select(x => $"{string.Format(KEYWORDTEMPLATE, x)}=@{x}")),
                string.Format(KEYWORDTEMPLATE, priKeyName),
                priKeyName);

            return result;
        }

        /// <summary>
        /// 检测字段
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Dictionary<string, bool> CheckPara<T>(T entity)
        {
            var result = new Dictionary<string, bool>();

            var props = entity.GetType().GetProperties();

            foreach (var item in props)
            {
                var regexAttr = item.GetCustomAttribute(typeof(CheckAttribute)) as CheckAttribute;

                if (regexAttr != null)
                {
                    var regex = regexAttr.Regex;
                    var value = item.GetValue(entity).ToString();

                    result.Add(item.Name, regex.IsMatch(value));
                }
            }

            return result;
        }

        /// <summary>
        /// 获得表名
        /// 若没有使用TableName 标明，则使用类名作为表名
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private string GetTableName<T>()
        {
            var type = typeof(T);
            var result = ((TableNameAttribute)type
                .GetCustomAttributes(typeof(TableNameAttribute), false).FirstOrDefault())
                ?.TableName ?? type.Name;
            return result;
        }

        /// <summary>
        /// 使用反射获得所有属性名，会忽略掉 通过VirtualFieldName将字段名指定为null的属性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private List<string> GetFieldNameList<T>()
        {
            var result = new List<string>();

            foreach (var item in typeof(T).GetProperties())
            {
                var fieldAttrArr = item.GetCustomAttributes(typeof(FieldNameAttribute), false);

                if (fieldAttrArr.Length == 0)
                {
                    result.Add(item.Name);
                }
                else
                {
                    var fieldAttr = (FieldNameAttribute)fieldAttrArr.First();

                    var fieldName = fieldAttr.FieldName;

                    if (!string.IsNullOrEmpty(fieldName))
                    {
                        result.Add(fieldName);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 获得标识为自增的属性名集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private List<string> GetIdentityFieldNameList<T>()
        {
            var result = new List<string>();
            foreach (var item in typeof(T).GetProperties())
            {
                var att = item.GetCustomAttribute(typeof(IdentityAttribute), false);

                if (att != null)
                {
                    result.Add(item.Name);
                }
            }

            return result;
        }

        /// <summary>
        /// 获得类中的主键名，如果没有使用PrimaryKey特性标示，则会寻找名为Id的属性，如果都没有，则会抛出异常
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private string GetPrimaryKeyName<T>()
        {
            var priKeyPropNames = new List<string>();

            var props = typeof(T).GetProperties();

            foreach (var item in props)
            {
                var priKeyAttrs = item.GetCustomAttributes(typeof(PrimaryKeyAttribute), false);

                if (priKeyAttrs.Length != 0)
                {
                    priKeyPropNames.Add(item.Name);
                }
            }

            if (priKeyPropNames.Count > 1)
            {
                throw new Exception("为一个类标示了多个主键:" + string.Join(",", priKeyPropNames.Select(x => x)));
            }

            if (priKeyPropNames.Count == 0)
            {
                //var idName = props.FirstOrDefault(x => string.Compare(x.Name, "id", true) == 0)?.Name;

                //if (string.IsNullOrEmpty(idName))
                //{
                //    throw new Exception($"无法识别主键，请添加属性Id或者使用{typeof(PrimaryKeyAttribute).FullName}来指定一个主键");
                //}

                //priKeyPropNames.Add(idName);
                throw new Exception("没有为类标识主键");
            }

            return priKeyPropNames.First();
        }
    }
}
