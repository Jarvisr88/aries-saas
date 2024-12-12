namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Utils;
    using DevExpress.XtraPrinting;
    using System;
    using System.Drawing;

    public abstract class GraphicsBase : IPrintingSystemContext, IServiceProvider
    {
        private PrintingSystemBase ps;
        private Page drawingPage;
        private DevExpress.XtraPrinting.Native.Measurer measurer;
        private IBrickPublisher brickPublisher;

        protected GraphicsBase(PrintingSystemBase ps);
        public virtual bool CanPublish(Brick brick);
        protected internal static void EnsureStringFormat(Font font, RectangleF bounds, GraphicsUnit unit, StringFormat format, Action<StringFormat> action);
        public Brush GetBrush(Color color);
        public virtual int GetPageCount(int basePageNumber, DefaultBoolean continuousPageNumbering);
        public object GetService(Type serviceType);
        public void ResetDrawingPage();
        public void SetDrawingPage(Page page);

        protected IBrickPublisher BrickPublisher { get; }

        public DevExpress.XtraPrinting.Native.Measurer Measurer { get; }

        public DevExpress.XtraPrinting.ProgressReflector ProgressReflector { get; }

        public PrintingSystemBase PrintingSystem { get; }

        public Page DrawingPage { get; }
    }
}

