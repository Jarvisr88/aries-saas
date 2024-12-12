namespace DevExpress.XtraPrintingLinks
{
    using DevExpress.XtraPrinting;
    using System;
    using System.ComponentModel;

    [DefaultProperty("Component")]
    public class PrintableComponentLinkBase : CorePrintableComponentLinkBase
    {
        public PrintableComponentLinkBase()
        {
        }

        public PrintableComponentLinkBase(PrintingSystemBase ps) : base(ps)
        {
        }

        public PrintableComponentLinkBase(IContainer container) : base(container)
        {
        }

        protected override void BeforeCreate()
        {
            base.BeforeCreate();
            base.ps.Document.CorrectImportBrickBounds = this.PrintableInternal.CreatesIntersectedBricks;
            if (this.PrintableInternal.HasPropertyEditor())
            {
                base.EnableCommand(PrintingSystemCommand.Customize, true);
            }
        }

        [DXHelpExclude(true), EditorBrowsable(EditorBrowsableState.Never)]
        public override void CreateDocument(bool buildForInstantPreview)
        {
            base.CreateDocument(buildForInstantPreview);
        }

        public override void SetDataObject(object data)
        {
            if (data is IPrintable)
            {
                this.PrintableInternal = data as IPrintable;
            }
        }

        private IPrintable PrintableInternal
        {
            get => 
                (IPrintable) base.ComponentBase;
            set => 
                base.ComponentBase = value;
        }

        public override Type PrintableObjectType =>
            typeof(IPrintable);

        [Description("Gets or sets a IPrintable user implementation printed via the current link."), Category("Printing"), DefaultValue((string) null)]
        public virtual IPrintable Component
        {
            get => 
                this.PrintableInternal;
            set => 
                this.PrintableInternal = value;
        }
    }
}

