namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Data.Access;
    using DevExpress.Xpf.Editors.Native;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Dynamic;
    using System.Windows.Forms;
    using System.Windows.Interop;

    public class SparklinePropertyDescriptor : SparklinePropertyDescriptorBase
    {
        private static readonly object Wrapped = new object();
        private readonly Dictionary<System.Type, SparklinePropertyDescriptor> descriptorsCache;
        private PropertyDescriptor BaseDescriptor;

        public SparklinePropertyDescriptor(string path, string internalPath) : base(path, internalPath)
        {
            this.descriptorsCache = new Dictionary<System.Type, SparklinePropertyDescriptor>();
        }

        private PropertyDescriptor CreateBaseDescriptor(object component) => 
            !IsComplexColumn(base.InternalPath) ? this.CreatePropertyAccessDescriptor(component) : new ComplexPropertyDescriptorReflection(component, base.InternalPath);

        protected PropertyDescriptor CreateFastPropertyDescriptor(PropertyDescriptor descriptor) => 
            DataListDescriptor.GetFastProperty(descriptor);

        private PropertyDescriptor CreatePropertyAccessDescriptor(object component)
        {
            if (component is DynamicObject)
            {
                return new DynamicObjectPropertyDescriptor(base.InternalPath);
            }
            if (component is ExpandoObject)
            {
                return new ExpandoPropertyDescriptor(null, base.InternalPath, null);
            }
            PropertyDescriptor descriptor = ListBindingHelper.GetListItemProperties(component)[base.InternalPath];
            descriptor ??= TypeDescriptor.GetProperties(component)[base.InternalPath];
            return ((descriptor == null) ? null : (this.ShouldCreateFastPropertyDescriptor ? this.CreateFastPropertyDescriptor(descriptor) : descriptor));
        }

        private SparklinePropertyDescriptor GetDescriptor(object component)
        {
            SparklinePropertyDescriptor descriptor;
            System.Type key = GetWrapped(component).GetType();
            this.descriptorsCache.TryGetValue(key, out descriptor);
            if ((descriptor == null) || !descriptor.IsRelevant(base.InternalPath))
            {
                descriptor = new SparklinePropertyDescriptor(base.Path, base.InternalPath);
                this.descriptorsCache[key] = descriptor;
            }
            return descriptor;
        }

        public override object GetValue(object component) => 
            !string.IsNullOrEmpty(base.InternalPath) ? this.GetDescriptor(component).GetValueImpl(component) : component;

        protected override object GetValueImpl(object component)
        {
            if (this.BaseDescriptor == null)
            {
                this.BaseDescriptor = this.CreateBaseDescriptor(component);
                if (this.BaseDescriptor == null)
                {
                    return null;
                }
            }
            return this.BaseDescriptor.GetValue(component);
        }

        private static object GetWrapped(object component) => 
            component ?? Wrapped;

        private static bool IsComplexColumn(string member) => 
            !string.IsNullOrEmpty(member) && member.Contains(".");

        public override void Reset()
        {
            if (this.descriptorsCache != null)
            {
                this.descriptorsCache.Clear();
            }
        }

        private bool ShouldCreateFastPropertyDescriptor =>
            !BrowserInteropHelper.IsBrowserHosted;
    }
}

