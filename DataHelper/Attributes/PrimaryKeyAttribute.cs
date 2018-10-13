using System;

namespace DataHelper.Attributes
{
    /// <summary>
    /// 标示字段为主键
    /// </summary>
    [System.AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class PrimaryKeyAttribute : Attribute
    {
        readonly string fieldName;

        // This is a positional argument
        public PrimaryKeyAttribute(string fieldName = null)
        {
            this.fieldName = fieldName;
        }

        public string FieldName
        {
            get { return fieldName; }
        }
    }
}
