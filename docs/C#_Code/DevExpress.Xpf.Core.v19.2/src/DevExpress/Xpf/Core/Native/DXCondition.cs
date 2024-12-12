namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Data;
    using System.Windows.Markup;

    [ContentProperty("TriggerValue")]
    public class DXCondition
    {
        public System.Windows.Data.Binding Binding { get; set; }

        public object TriggerValue { get; set; }
    }
}

