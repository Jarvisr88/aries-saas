namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Data;

    internal class BindingSource
    {
        public BindingSource(string elementName, System.Windows.Data.RelativeSource relativeSource);

        public string ElementName { get; set; }

        public Binding NewBinding { get; set; }

        public System.Windows.Data.RelativeSource RelativeSource { get; set; }
    }
}

