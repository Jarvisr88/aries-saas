namespace DevExpress.Xpf.Editors.Internal
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public abstract class SparklinePropertyDescriptorBase : PropertyDescriptor
    {
        public static readonly object UnsetValue = new object();

        protected SparklinePropertyDescriptorBase(string path, string internalPath) : base(path, null)
        {
            this.Path = path;
            this.InternalPath = internalPath;
        }

        public override bool CanResetValue(object component) => 
            false;

        public static SparklinePropertyDescriptorBase CreatePropertyDescriptor(Type componentType, string path, string internalPath = null) => 
            new SparklinePropertyDescriptor(path, internalPath);

        public override object GetValue(object component) => 
            this.GetValueImpl(component);

        protected abstract object GetValueImpl(object component);
        public bool IsRelevant(string internalPath) => 
            (internalPath != null) && ((this.InternalPath != null) && (internalPath == this.InternalPath));

        public static bool IsUnsetValue(object value) => 
            UnsetValue == value;

        public virtual void Reset()
        {
        }

        public override void ResetValue(object component)
        {
        }

        public override void SetValue(object component, object value)
        {
        }

        public override bool ShouldSerializeValue(object component) => 
            false;

        protected string Path { get; private set; }

        protected string InternalPath { get; private set; }

        public override string DisplayName =>
            this.Path;

        public override Type ComponentType =>
            typeof(object);

        public override string Name =>
            this.Path;

        public override bool IsReadOnly =>
            false;

        public override Type PropertyType =>
            typeof(object);
    }
}

