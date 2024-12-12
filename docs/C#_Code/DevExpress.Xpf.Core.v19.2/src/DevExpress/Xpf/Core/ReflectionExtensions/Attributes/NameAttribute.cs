namespace DevExpress.Xpf.Core.ReflectionExtensions.Attributes
{
    using System;

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method)]
    public class NameAttribute : Attribute
    {
        private readonly string name;

        public NameAttribute(string name)
        {
            this.name = name;
        }

        public string Name =>
            this.name;
    }
}

