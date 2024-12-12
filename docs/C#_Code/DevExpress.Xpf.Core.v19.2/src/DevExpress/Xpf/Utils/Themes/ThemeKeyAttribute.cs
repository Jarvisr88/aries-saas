namespace DevExpress.Xpf.Utils.Themes
{
    using System;
    using System.Runtime.CompilerServices;

    [AttributeUsage(AttributeTargets.Class)]
    public class ThemeKeyAttribute : Attribute
    {
        public ThemeKeyAttribute(Type targetType)
        {
            this.TargetType = targetType;
        }

        public Type TargetType { get; set; }
    }
}

