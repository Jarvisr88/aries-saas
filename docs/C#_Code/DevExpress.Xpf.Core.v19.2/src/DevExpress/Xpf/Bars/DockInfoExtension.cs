namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Markup;

    public class DockInfoExtension : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider);

        public BarContainerType ContainerType { get; set; }

        public string ContainerName { get; set; }

        public int Row { get; set; }

        public int Column { get; set; }

        public double Offset { get; set; }
    }
}

