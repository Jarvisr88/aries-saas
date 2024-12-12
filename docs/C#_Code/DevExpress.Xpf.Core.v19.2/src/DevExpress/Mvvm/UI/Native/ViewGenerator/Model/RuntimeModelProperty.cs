namespace DevExpress.Mvvm.UI.Native.ViewGenerator.Model
{
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Markup;

    public class RuntimeModelProperty : ModelPropertyBase
    {
        public RuntimeModelProperty(EditingContextBase context, object obj, PropertyDescriptor property, IModelItem parent) : base(context, obj, property, parent)
        {
        }

        protected override IModelItem GetValue() => 
            base.context.CreateModelItem(this.GetComputedValue(), base.Parent);

        protected override bool IsPropertySet()
        {
            DependencyPropertyDescriptor descriptor = DependencyPropertyDescriptor.FromProperty(base.property);
            DependencyObject o = base.obj as DependencyObject;
            return (((descriptor == null) || (o == null)) ? base.IsPropertySet() : DependencyObjectPropertyHelper.IsPropertyAssigned(o, descriptor.DependencyProperty));
        }

        protected override IModelItem SetValueCore(object value)
        {
            if (value is BindingBase)
            {
                DependencyPropertyDescriptor descriptor = DependencyPropertyDescriptor.FromProperty(base.property);
                DependencyObject target = base.obj as DependencyObject;
                if ((descriptor != null) && (target != null))
                {
                    BindingOperations.SetBinding(target, descriptor.DependencyProperty, (BindingBase) value);
                    return null;
                }
            }
            if (value is IModelItem)
            {
                value = ((IModelItem) value).GetCurrentValue();
            }
            if (value is MarkupExtension)
            {
                value = ((MarkupExtension) value).ProvideValue(null);
            }
            base.property.SetValue(base.obj, value);
            return null;
        }
    }
}

