using System;
using System.Text.RegularExpressions;

namespace DataHelper.Attributes
{
    [System.AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    sealed class CheckAttribute : Attribute
    {
        readonly string regexStr;

        public CheckAttribute(string regex)
        {
            regexStr = regex;
        }

        public Regex Regex
        {
            get { return new Regex(regexStr); }
        }

        // This is a named argument
        public int NamedInt { get; set; }
    }
}
