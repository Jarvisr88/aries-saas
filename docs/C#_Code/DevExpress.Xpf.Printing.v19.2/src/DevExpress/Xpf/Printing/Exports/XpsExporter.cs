namespace DevExpress.Xpf.Printing.Exports
{
    using DevExpress.Printing.Native;
    using DevExpress.Utils;
    using DevExpress.Xpf.Printing.Native;
    using DevExpress.XtraPrinting;
    using System;
    using System.IO;
    using System.IO.Packaging;
    using System.Runtime.CompilerServices;
    using System.Security;
    using System.Threading;
    using System.Windows.Documents;
    using System.Windows.Documents.Serialization;
    using System.Windows.Xps;
    using System.Windows.Xps.Packaging;

    internal class XpsExporter
    {
        public event EventHandler ProgressChanged;

        [SecuritySafeCritical]
        public void CreateDocument(DocumentPaginator paginator, Stream stream, XpsExportOptions options)
        {
            Guard.ArgumentNotNull(paginator, "paginator");
            Guard.ArgumentNotNull(stream, "stream");
            Guard.ArgumentNotNull(options, "options");
            int[] indices = PageRangeParser.GetIndices(options.PageRange, paginator.PageCount);
            PageRangeCustomPaginator documentPaginator = new PageRangeCustomPaginator(paginator, indices);
            using (Package package = Package.Open(stream, FileMode.Create, FileAccess.ReadWrite))
            {
                string uriString = $"pack://document{Guid.NewGuid().ToString("N")}.xps";
                PackageStore.AddPackage(new Uri(uriString), package);
                SetPackageProperties(options, package);
                try
                {
                    using (XpsDocument document = new XpsDocument(package, (CompressionOption) options.Compression, uriString))
                    {
                        XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(document);
                        writer.WritingProgressChanged += new WritingProgressChangedEventHandler(this.Writer_WritingProgressChanged);
                        writer.Write(documentPaginator);
                    }
                }
                finally
                {
                    Uri uri;
                    PackageStore.RemovePackage(uri);
                }
            }
        }

        private static void SetPackageProperties(XpsExportOptions options, Package package)
        {
            if (options.DocumentOptions != null)
            {
                PackageProperties packageProperties = package.PackageProperties;
                XpsDocumentOptions documentOptions = options.DocumentOptions;
                packageProperties.Creator = documentOptions.Creator;
                packageProperties.Category = documentOptions.Category;
                packageProperties.Title = documentOptions.Title;
                packageProperties.Subject = documentOptions.Subject;
                packageProperties.Keywords = documentOptions.Keywords;
                packageProperties.Version = documentOptions.Version;
                packageProperties.Description = documentOptions.Description;
            }
        }

        private void Writer_WritingProgressChanged(object sender, WritingProgressChangedEventArgs e)
        {
            if (this.ProgressChanged != null)
            {
                this.ProgressChanged(this, EventArgs.Empty);
            }
        }
    }
}

