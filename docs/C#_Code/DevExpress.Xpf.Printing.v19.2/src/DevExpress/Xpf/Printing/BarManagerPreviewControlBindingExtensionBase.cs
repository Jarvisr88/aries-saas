namespace DevExpress.Xpf.Printing
{
    using DevExpress.Xpf.Bars;
    using System;
    using System.Windows;

    public abstract class BarManagerPreviewControlBindingExtensionBase : FindAncestorBindingExtension
    {
        protected BarManagerPreviewControlBindingExtensionBase()
        {
        }

        protected override Type AncestorType =>
            typeof(BarManager);

        protected override PropertyPath BindingPath
        {
            get
            {
                if (string.IsNullOrEmpty(base.Path))
                {
                    return new PropertyPath(this.ControlProperty);
                }
                object[] pathParameters = new object[] { this.ControlProperty };
                return new PropertyPath("(0)." + base.Path, pathParameters);
            }
        }

        protected abstract DependencyProperty ControlProperty { get; }
    }
}

