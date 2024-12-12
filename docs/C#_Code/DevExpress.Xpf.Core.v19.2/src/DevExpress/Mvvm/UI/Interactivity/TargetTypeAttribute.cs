namespace DevExpress.Mvvm.UI.Interactivity
{
    using System;
    using System.Runtime.CompilerServices;

    [AttributeUsage(AttributeTargets.Class, AllowMultiple=true, Inherited=true)]
    public sealed class TargetTypeAttribute : Attribute
    {
        public TargetTypeAttribute(Type targetType) : this(true, targetType)
        {
        }

        public TargetTypeAttribute(bool isTargetType, Type targetType)
        {
            this.IsTargetType = isTargetType;
            this.TargetType = targetType;
        }

        public bool IsTargetType { get; private set; }

        public Type TargetType { get; private set; }
    }
}

