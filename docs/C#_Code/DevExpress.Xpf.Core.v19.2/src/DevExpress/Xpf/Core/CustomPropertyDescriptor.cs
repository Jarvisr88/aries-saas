namespace DevExpress.Xpf.Core
{
    using System;
    using System.ComponentModel;

    public abstract class CustomPropertyDescriptor : PropertyDescriptor
    {
        protected CustomPropertyDescriptor(string name) : base(name, null)
        {
        }

        public override bool CanResetValue(object component) => 
            false;

        public override void ResetValue(object component)
        {
        }

        public override bool ShouldSerializeValue(object component) => 
            false;

        public override Type ComponentType =>
            typeof(object);
    }
}

