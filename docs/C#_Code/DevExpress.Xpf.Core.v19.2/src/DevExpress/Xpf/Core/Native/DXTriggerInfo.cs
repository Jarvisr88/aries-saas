namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Windows.Data;
    using System.Windows.Markup;

    [ContentProperty("TriggerValue")]
    public class DXTriggerInfo : DXTriggerInfoBase
    {
        private object triggerValue;
        private System.Windows.Data.Binding binding;

        public DXTriggerInfo();

        public object TriggerValue { get; set; }

        public System.Windows.Data.Binding Binding { get; set; }
    }
}

