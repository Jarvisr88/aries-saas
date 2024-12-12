namespace DevExpress.Mvvm.UI.Native.ViewGenerator.Model
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class RuntimeModelPropertyCollection : ModelPropertyCollectionBase
    {
        private readonly PropertyDescriptorCollection properties;

        public RuntimeModelPropertyCollection(EditingContextBase context, object obj, IModelItem parent) : base(context, obj, parent)
        {
            Func<object, PropertyDescriptorCollection> evaluator = <>c.<>9__1_0;
            if (<>c.<>9__1_0 == null)
            {
                Func<object, PropertyDescriptorCollection> local1 = <>c.<>9__1_0;
                evaluator = <>c.<>9__1_0 = x => TypeDescriptor.GetProperties(x.GetType());
            }
            this.properties = obj.Return<object, PropertyDescriptorCollection>(evaluator, <>c.<>9__1_1 ??= () => PropertyDescriptorCollection.Empty);
        }

        protected override IModelProperty FindCore(string propertyName, Type type)
        {
            PropertyDescriptor property = this.properties[propertyName];
            return ((property != null) ? base.context.CreateModelProperty(base.obj, property, base.parent) : null);
        }

        public override IEnumerator<IModelProperty> GetEnumerator()
        {
            List<IModelProperty> list = new List<IModelProperty>();
            foreach (PropertyDescriptor descriptor in this.properties)
            {
                list.Add(new RuntimeModelProperty(base.context, base.obj, descriptor, base.parent));
            }
            return list.GetEnumerator();
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly RuntimeModelPropertyCollection.<>c <>9 = new RuntimeModelPropertyCollection.<>c();
            public static Func<object, PropertyDescriptorCollection> <>9__1_0;
            public static Func<PropertyDescriptorCollection> <>9__1_1;

            internal PropertyDescriptorCollection <.ctor>b__1_0(object x) => 
                TypeDescriptor.GetProperties(x.GetType());

            internal PropertyDescriptorCollection <.ctor>b__1_1() => 
                PropertyDescriptorCollection.Empty;
        }
    }
}

