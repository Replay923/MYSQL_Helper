using System;

namespace DataHelper.Attributes
{
    [System.AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class IdentityAttribute : Attribute
    {
        public IdentityAttribute() { }
    }
}
