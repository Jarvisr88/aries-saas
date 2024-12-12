namespace DevExpress.Utils.Design
{
    using System;

    [AttributeUsage(AttributeTargets.Class)]
    public sealed class DefaultBindingPropertyExAttribute : Attribute
    {
        public DefaultBindingPropertyExAttribute(string name)
        {
        }
    }
}

