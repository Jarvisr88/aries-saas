namespace DevExpress.Data.Browsing
{
    using DevExpress.XtraReports.Native;
    using System;
    using System.ComponentModel;

    public class DisplayPropertyDescriptor : PropertyDescriptorWrapper
    {
        private string displayName;

        public DisplayPropertyDescriptor(PropertyDescriptor descr, string displayName);

        public override string DisplayName { get; }
    }
}

