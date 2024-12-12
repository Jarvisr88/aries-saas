namespace DevExpress.Xpf.Grid
{
    using System;
    using System.ComponentModel;

    internal interface ISimpleBindingProcessor
    {
        object GetValue(object row);
        void SetValue(object row, object newValue);

        PropertyDescriptor DescriptorToListen { get; }

        PropertyDescriptor DataControllerDescriptor { get; }
    }
}

