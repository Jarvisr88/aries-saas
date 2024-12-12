namespace DevExpress.Xpf.Core.ReflectionExtensions.Attributes
{
    using System;

    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Property | AttributeTargets.Method)]
    public class FallbackModeAttribute : Attribute
    {
        private readonly ReflectionHelperFallbackMode mode;

        public FallbackModeAttribute(ReflectionHelperFallbackMode mode)
        {
            this.mode = mode;
        }

        public ReflectionHelperFallbackMode Mode =>
            this.mode;
    }
}

