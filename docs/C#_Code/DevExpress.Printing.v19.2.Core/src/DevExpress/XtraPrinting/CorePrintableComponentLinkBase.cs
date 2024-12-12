namespace DevExpress.XtraPrinting
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    [ToolboxItem(false), DesignTimeVisible(false)]
    public abstract class CorePrintableComponentLinkBase : LinkBase
    {
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override event CreateAreaEventHandler CreateDetailArea
        {
            add
            {
            }
            remove
            {
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override event CreateAreaEventHandler CreateDetailFooterArea
        {
            add
            {
            }
            remove
            {
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override event CreateAreaEventHandler CreateDetailHeaderArea
        {
            add
            {
            }
            remove
            {
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override event CreateAreaEventHandler CreateInnerPageFooterArea
        {
            add
            {
            }
            remove
            {
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override event CreateAreaEventHandler CreateInnerPageHeaderArea
        {
            add
            {
            }
            remove
            {
            }
        }

        public CorePrintableComponentLinkBase()
        {
        }

        public CorePrintableComponentLinkBase(PrintingSystemBase ps) : base(ps)
        {
        }

        public CorePrintableComponentLinkBase(IContainer container) : base(container)
        {
        }

        public override void AddSubreport(PointF offset)
        {
            if (this.ComponentBase != null)
            {
                base.AddSubreport(offset);
            }
        }

        protected override void BeforeCreate()
        {
            if (this.ComponentBase == null)
            {
                throw new NullReferenceException("The Component property value must not be null");
            }
            base.BeforeCreate();
            base.ps.Graph.PageUnit = GraphicsUnit.Pixel;
            this.ComponentBase.Initialize(base.ps, this);
        }

        internal override void BeforeDestroy()
        {
            if (this.ComponentBase != null)
            {
                this.ComponentBase.Finalize(base.ps, this);
            }
            base.BeforeDestroy();
        }

        protected override bool CanHandleCommandInternal(PrintingSystemCommand command, IPrintControl printControl) => 
            base.CanHandleCommandInternal(command, printControl) || (command == PrintingSystemCommand.Customize);

        protected override void CreateDetail(BrickGraphics graph)
        {
            this.ComponentBase.CreateArea("Detail", base.ps.Graph);
        }

        protected override void CreateDetailFooter(BrickGraphics graph)
        {
            this.ComponentBase.CreateArea("DetailFooter", base.ps.Graph);
        }

        protected override void CreateDetailHeader(BrickGraphics graph)
        {
            this.ComponentBase.CreateArea("DetailHeader", base.ps.Graph);
        }

        protected override void CreateInnerPageFooter(BrickGraphics graph)
        {
            this.ComponentBase.CreateArea("InnerPageFooter", base.ps.Graph);
            base.CreateInnerPageFooter(graph);
        }

        protected override void CreateInnerPageHeader(BrickGraphics graph)
        {
            this.ComponentBase.CreateArea("InnerPageHeader", base.ps.Graph);
            base.CreateInnerPageHeader(graph);
        }

        protected override void CreateMarginalFooter(BrickGraphics graph)
        {
            base.CreateMarginalFooter(graph);
            this.ComponentBase.CreateArea("MarginalFooter", base.ps.Graph);
        }

        protected override void CreateMarginalHeader(BrickGraphics graph)
        {
            base.CreateMarginalHeader(graph);
            this.ComponentBase.CreateArea("MarginalHeader", base.ps.Graph);
        }

        protected override void CreateReportFooter(BrickGraphics graph)
        {
            this.ComponentBase.CreateArea("ReportFooter", base.ps.Graph);
            base.CreateReportFooter(graph);
        }

        protected override void CreateReportHeader(BrickGraphics graph)
        {
            this.ComponentBase.CreateArea("ReportHeader", base.ps.Graph);
            base.CreateReportHeader(graph);
        }

        protected override void DisableCommands()
        {
            base.DisableCommands();
            base.EnableCommand(PrintingSystemCommand.Customize, false);
        }

        protected override void OnEndActivity()
        {
            if (this.ComponentEx != null)
            {
                this.ComponentEx.OnEndActivity();
            }
        }

        protected override void OnStartActivity()
        {
            if (this.ComponentEx != null)
            {
                this.ComponentEx.OnStartActivity();
            }
        }

        public override void SetDataObject(object data)
        {
            IBasePrintable printable = data as IBasePrintable;
            if (printable != null)
            {
                this.ComponentBase = printable;
            }
        }

        protected IBasePrintable ComponentBase { get; set; }

        protected override string InnerPageHeader =>
            (this.ComponentBase is IPrintHeaderFooter) ? ((IPrintHeaderFooter) this.ComponentBase).InnerPageHeader : string.Empty;

        protected override string InnerPageFooter =>
            (this.ComponentBase is IPrintHeaderFooter) ? ((IPrintHeaderFooter) this.ComponentBase).InnerPageFooter : string.Empty;

        protected override string ReportHeader
        {
            get
            {
                IPrintHeaderFooter componentBase = this.ComponentBase as IPrintHeaderFooter;
                return (((componentBase == null) || string.IsNullOrEmpty(componentBase.ReportHeader)) ? base.ReportHeader : componentBase.ReportHeader);
            }
        }

        protected override string ReportFooter
        {
            get
            {
                IPrintHeaderFooter componentBase = this.ComponentBase as IPrintHeaderFooter;
                return (((componentBase == null) || string.IsNullOrEmpty(componentBase.ReportFooter)) ? base.ReportFooter : componentBase.ReportFooter);
            }
        }

        public override Type PrintableObjectType =>
            typeof(IBasePrintable);

        protected override BrickModifier InternalSkipArea =>
            BrickModifier.None;

        protected IPrintableEx ComponentEx =>
            this.ComponentBase as IPrintableEx;
    }
}

