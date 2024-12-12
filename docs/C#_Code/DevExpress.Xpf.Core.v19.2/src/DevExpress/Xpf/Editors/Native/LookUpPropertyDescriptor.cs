namespace DevExpress.Xpf.Editors.Native
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    public class LookUpPropertyDescriptor : LookUpPropertyDescriptorBase
    {
        private static readonly object Wrapped = new object();
        private readonly Dictionary<Type, LookUpPropertyDescriptorBase> descriptorsCache;

        public LookUpPropertyDescriptor(LookUpPropertyDescriptorType descriptorType, string path, string internalPath) : base(descriptorType, path, internalPath)
        {
            this.descriptorsCache = new Dictionary<Type, LookUpPropertyDescriptorBase>();
        }

        private PropertyDescriptor GetDescriptor(object component)
        {
            LookUpPropertyDescriptorBase base2;
            Type key = GetWrapped(component).GetType();
            this.descriptorsCache.TryGetValue(key, out base2);
            if ((base2 == null) || !base2.IsRelevant(base.InternalPath))
            {
                base2 = CreatePropertyDescriptor(key, base.DescriptorType, base.Path, base.InternalPath);
                this.descriptorsCache[key] = base2;
            }
            return base2;
        }

        protected override object GetValueImpl(object component) => 
            this.GetDescriptor(component).GetValue(component);

        private static object GetWrapped(object component) => 
            component ?? Wrapped;

        public override void Reset()
        {
            if (this.descriptorsCache != null)
            {
                this.descriptorsCache.Clear();
            }
        }

        protected override void SetValueImpl(object component, object value)
        {
            this.GetDescriptor(component).SetValue(component, value);
        }
    }
}

