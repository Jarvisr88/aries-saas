namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Xpf.DocumentViewer;
    using DevExpress.Xpf.Grid;
    using DevExpress.Xpf.Grid.TreeList;
    using System.Reflection;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    public class PdfOutlinesViewerNodeSelector : TreeListNodeImageSelector
    {
        public override ImageSource Select(TreeListRowData rowData) => 
            new BitmapImage(UriHelper.GetUri(Assembly.GetExecutingAssembly().GetName().Name, @"Images\Outlines\Bookmark_16x16.png"));
    }
}

