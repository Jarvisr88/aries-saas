namespace DevExpress.XtraPrinting.Export
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.BrickExporters;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Drawing;

    public abstract class PageLayoutBuilder : ILayoutBuilder, IDisposable
    {
        private DevExpress.XtraPrinting.Page page;
        protected LayoutControlCollection layoutControls;
        protected DevExpress.XtraPrinting.Native.ExportContext exportContext;

        protected PageLayoutBuilder(DevExpress.XtraPrinting.Page page, DevExpress.XtraPrinting.Native.ExportContext exportContext)
        {
            exportContext.SetDrawingPage(page);
            this.page = page;
            this.exportContext = exportContext;
        }

        internal void AddData(BrickViewData[] data)
        {
            foreach (BrickViewData data2 in data)
            {
                this.layoutControls.Add(this.ValidateViewData(data2));
            }
        }

        public LayoutControlCollection BuildLayoutControls()
        {
            this.layoutControls = new LayoutControlCollection();
            RectangleF clipRect = new RectangleF(this.InitialLayoutLocation, this.page.PageSize);
            (BrickBaseExporter.GetExporter(this.exportContext.PrintingSystem, this.page) as PageExporter).ProcessLayout(this, clipRect.Location, clipRect);
            return this.layoutControls;
        }

        public void Dispose()
        {
            this.exportContext.ResetDrawingPage();
        }

        internal abstract RectangleF GetCorrectClipRect(RectangleF clipRect);
        internal abstract BrickViewData[] GetData(Brick brick, RectangleF bounds, RectangleF clipRect);
        protected virtual ILayoutControl ValidateViewData(BrickViewData viewData) => 
            LayoutControl.Validate(viewData);

        protected DevExpress.XtraPrinting.Page Page =>
            this.page;

        protected virtual PointF InitialLayoutLocation =>
            PointF.Empty;

        internal DevExpress.XtraPrinting.Native.ExportContext ExportContext =>
            this.exportContext;
    }
}

