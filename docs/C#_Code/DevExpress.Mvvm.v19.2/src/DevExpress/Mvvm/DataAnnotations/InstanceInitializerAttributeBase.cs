namespace DevExpress.Mvvm.DataAnnotations
{
    using System;
    using System.Runtime.CompilerServices;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple=true)]
    public abstract class InstanceInitializerAttributeBase : Attribute
    {
        private readonly Func<object> createInstanceCallback;

        protected InstanceInitializerAttributeBase(System.Type type) : this(type, type.Name, null)
        {
        }

        protected InstanceInitializerAttributeBase(System.Type type, string name, Func<object> createInstanceCallback)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }
            this.Type = type;
            this.Name = name;
            this.createInstanceCallback = createInstanceCallback;
        }

        public virtual object CreateInstance() => 
            (this.createInstanceCallback != null) ? this.createInstanceCallback() : Activator.CreateInstance(this.Type);

        public string Name { get; private set; }

        public System.Type Type { get; private set; }

        public override object TypeId =>
            this.Name;
    }
}

