using System;

namespace DataHelper.Attributes
{
    /// <summary>
    /// 该特性表示属性为一个虚拟的view字段，若没有为空或空字符串，则不在sql语句中生成关于该字段的sql，否则将会以描述名称来生成sql
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class FieldNameAttribute : Attribute
    {
        readonly string fieldName;

        // This is a positional argument
        /// <summary>
        /// 虚拟字段
        /// </summary>
        /// <remarks>参数可为空</remarks>
        /// <param name="fieldName"></param>
        public FieldNameAttribute(string fieldName)
        {
            this.fieldName = fieldName;
        }

        /// <summary>
        /// 字段名
        /// </summary>
        public string FieldName
        {
            get { return fieldName; }
        }
    }
}
