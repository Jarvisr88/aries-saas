namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;

    internal class PrintingSystemContextWrapper : IPrintingSystemContext, IServiceProvider
    {
        private readonly IPrintingSystemContext context;
        private readonly Page drawingPage;

        public PrintingSystemContextWrapper(IPrintingSystemContext context, Page drawingPage)
        {
            this.context = context;
            this.drawingPage = drawingPage;
        }

        public bool CanPublish(Brick brick) => 
            this.context.CanPublish(brick);

        public object GetService(Type serviceType) => 
            this.context.GetService(serviceType);

        public Page DrawingPage =>
            this.drawingPage;

        public PrintingSystemBase PrintingSystem =>
            this.context.PrintingSystem;

        public DevExpress.XtraPrinting.Native.Measurer Measurer =>
            this.context.Measurer;
    }
}

