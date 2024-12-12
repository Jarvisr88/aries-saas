namespace DevExpress.Xpf.Printing
{
    using DevExpress.Utils;
    using DevExpress.Xpf.Printing.Native;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.ComponentModel;

    public class PrintingSystem : PrintingSystemBase
    {
        private IDocumentFactory documentFactory;

        public PrintingSystem() : base(null, new ExportOptionsContainer())
        {
            this.documentFactory = new PrintingDocumentFactory();
            base.AddService(typeof(BackgroundPageBuildEngineStrategy), new DispatcherPageBuildStrategy());
        }

        protected override PrintingDocument CreateDocument() => 
            this.DocumentFactory.Create(this);

        public void ExportToXps(string filePath)
        {
            this.ExportToXps(filePath, this.ExportOptions.Xps);
        }

        public override CommandVisibility GetCommandVisibility(PrintingSystemCommand command) => 
            CommandVisibility.All;

        internal IDocumentFactory DocumentFactory
        {
            get => 
                this.documentFactory;
            set
            {
                Guard.ArgumentNotNull(value, "value");
                this.documentFactory = value;
            }
        }

        [Description("Gets the settings used to specify export parameters when exporting a printing system's document.")]
        public ExportOptionsContainer ExportOptions =>
            (ExportOptionsContainer) base.ExportOptions;
    }
}

