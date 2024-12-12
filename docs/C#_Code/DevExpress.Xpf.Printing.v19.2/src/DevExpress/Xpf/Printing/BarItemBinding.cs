namespace DevExpress.Xpf.Printing
{
    using DevExpress.Xpf.Bars;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Data;

    public class BarItemBinding : BindingExtensionBase
    {
        public BarItemBinding()
        {
            this.IsBarItemTarget = true;
        }

        public bool IsBarItemTarget { get; set; }

        protected override PropertyPath BindingPath
        {
            get
            {
                object[] pathParameters = new object[] { BarManager.BarManagerProperty, DocumentViewer.DocumentViewerProperty };
                return new PropertyPath($"(0).(1).{base.Path}", pathParameters);
            }
        }

        protected override System.Windows.Data.RelativeSource RelativeSource
        {
            get
            {
                if (!this.IsBarItemTarget)
                {
                    return null;
                }
                System.Windows.Data.RelativeSource source1 = new System.Windows.Data.RelativeSource();
                source1.Mode = RelativeSourceMode.Self;
                return source1;
            }
        }
    }
}

