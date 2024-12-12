namespace DevExpress.Xpf.Printing
{
    using DevExpress.XtraPrinting.DataNodes;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class GroupInfo
    {
        public GroupInfo() : this(null)
        {
        }

        public GroupInfo(DataTemplate headerTemplate) : this(headerTemplate, null)
        {
        }

        public GroupInfo(DataTemplate headerTemplate, DataTemplate footerTemplate)
        {
            this.HeaderTemplate = headerTemplate;
            this.FooterTemplate = footerTemplate;
        }

        [Description("Gets or sets a template which defines how data is represented in the group header area.")]
        public DataTemplate HeaderTemplate { get; set; }

        [Description("Specifies the group footer template for GroupInfo.")]
        public DataTemplate FooterTemplate { get; set; }

        public bool PageBreakBefore { get; set; }

        public bool PageBreakAfter { get; set; }

        public GroupUnion Union { get; set; }

        public bool RepeatHeaderEveryPage { get; set; }
    }
}

