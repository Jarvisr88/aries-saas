namespace DevExpress.Mvvm.DataAnnotations
{
    using System;

    public class InstanceInitializerAttribute : InstanceInitializerAttributeBase
    {
        public InstanceInitializerAttribute(Type type) : base(type)
        {
        }

        public InstanceInitializerAttribute(Type type, string name) : base(type, name, null)
        {
        }

        internal InstanceInitializerAttribute(Type type, string name, Func<object> createInstanceCallback) : base(type, name, createInstanceCallback)
        {
        }
    }
}

