namespace DevExpress.XtraPrintingLinks
{
    using DevExpress.XtraPrinting;
    using System;
    using System.ComponentModel;

    [ToolboxItem(false), DesignTimeVisible(false)]
    public class PrintableComponentLinkCore : CorePrintableComponentLinkBase
    {
        public PrintableComponentLinkCore()
        {
        }

        public PrintableComponentLinkCore(PrintingSystemBase ps) : base(ps)
        {
        }

        public PrintableComponentLinkCore(IContainer container) : base(container)
        {
        }

        public virtual IBasePrintable Component
        {
            get => 
                base.ComponentBase;
            set => 
                base.ComponentBase = value;
        }
    }
}

