namespace DevExpress.Xpf.Printing.PreviewControl.Bars
{
    using DevExpress.Xpf.Printing.Themes;

    public class SendSplitItem : ExportSplitItem
    {
        protected override NewDocumentViewerThemeKeys GetItemTemplateKey() => 
            NewDocumentViewerThemeKeys.SendPopupMenuItemTemplate;
    }
}

