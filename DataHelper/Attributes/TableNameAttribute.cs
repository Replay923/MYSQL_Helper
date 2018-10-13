using System;

namespace DataHelper.Attributes
{
    /// <summary>
    /// 该特性表明了该类可以用来生成sql语句，参数为空的情况下，则使用该类的名称作为表名
    /// </summary>
    [System.AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public sealed class TableNameAttribute : Attribute
    {
        readonly string tableName;

        /// <summary>
        /// 指明表名
        /// </summary>
        /// <param name="tableName"></param>
        public TableNameAttribute(string tableName = null)
        {
            if (string.IsNullOrEmpty(tableName))
                tableName = this.GetType().Name;
            this.tableName = tableName;
        }

        public string TableName
        {
            get { return tableName; }
        }
    }
}
