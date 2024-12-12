namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using DevExpress.XtraPrinting.XamlExport;
    using System;
    using System.IO;
    using System.Windows;

    public class BrickPageVisualizer : PageVisualizer
    {
        private readonly TextMeasurementSystem textMeasurementSystem;

        public BrickPageVisualizer(TextMeasurementSystem textMeasurementSystem)
        {
            this.textMeasurementSystem = textMeasurementSystem;
        }

        public Stream SaveToStream(Page page, int pageIndex, int pageCount)
        {
            if (page == null)
            {
                throw new ArgumentNullException("page");
            }
            MemoryStream stream = new MemoryStream();
            new XamlExporter().Export(stream, page, pageIndex + 1, pageCount, this.XamlCompatibility, this.textMeasurementSystem);
            stream.Position = 0L;
            return stream;
        }

        public override FrameworkElement Visualize(PSPage page, int pageIndex, int pageCount) => 
            XamlReaderHelper.Load(this.SaveToStream(page, pageIndex, pageCount));

        protected DevExpress.XtraPrinting.XamlExport.XamlCompatibility XamlCompatibility =>
            DevExpress.XtraPrinting.XamlExport.XamlCompatibility.WPF;
    }
}

