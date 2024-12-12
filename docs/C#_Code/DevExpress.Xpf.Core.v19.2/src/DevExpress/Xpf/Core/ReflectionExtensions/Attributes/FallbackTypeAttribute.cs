namespace DevExpress.Xpf.Core.ReflectionExtensions.Attributes
{
    using System;

    [AttributeUsage(AttributeTargets.Interface)]
    public class FallbackTypeAttribute : Attribute
    {
        private readonly System.Type type;

        public FallbackTypeAttribute(System.Type type)
        {
            this.type = type;
        }

        public System.Type Type =>
            this.type;
    }
}

