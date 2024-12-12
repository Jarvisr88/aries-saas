namespace DevExpress.Xpf.Core.ReflectionExtensions.Attributes
{
    using System;
    using System.Reflection;

    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Event | AttributeTargets.Property | AttributeTargets.Method)]
    public class BindingFlagsAttribute : Attribute
    {
        private readonly BindingFlags flags;

        public BindingFlagsAttribute(BindingFlags flags)
        {
            this.flags = flags;
        }

        public BindingFlags Flags =>
            this.flags;
    }
}

