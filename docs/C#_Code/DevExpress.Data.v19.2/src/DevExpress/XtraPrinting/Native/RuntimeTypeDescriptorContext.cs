namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Collections;
    using System.ComponentModel;

    public class RuntimeTypeDescriptorContext : ITypeDescriptorContext, IServiceProvider
    {
        private object instance;
        private System.ComponentModel.PropertyDescriptor propDesc;

        public RuntimeTypeDescriptorContext(System.ComponentModel.PropertyDescriptor propDesc, object instance)
        {
            this.propDesc = propDesc;
            this.instance = instance;
        }

        public virtual object GetService(Type serviceType) => 
            !(serviceType == typeof(System.ComponentModel.PropertyDescriptor)) ? null : this.propDesc;

        public static object[] GetStandardValues(ITypeDescriptorContext context, TypeConverter converter)
        {
            object[] array = null;
            if ((converter != null) && converter.GetStandardValuesSupported(context))
            {
                ICollection standardValues = converter.GetStandardValues(context);
                array = new object[standardValues.Count];
                standardValues.CopyTo(array, 0);
            }
            return array;
        }

        public virtual void OnComponentChanged()
        {
        }

        public virtual bool OnComponentChanging() => 
            true;

        public object Instance =>
            this.instance;

        public System.ComponentModel.PropertyDescriptor PropertyDescriptor =>
            this.propDesc;

        public virtual IContainer Container =>
            null;
    }
}

