namespace DevExpress.Xpf.Printing
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.UI;
    using System;
    using System.Drawing.Printing;
    using System.Windows;

    public class PrintableControlPreviewService : ServiceBase, IPrintableControlPreviewService
    {
        public static readonly DependencyProperty LandscapeProperty = DependencyProperty.Register("Landscape", typeof(bool), typeof(PrintableControlPreviewService), new PropertyMetadata(false));
        public static readonly DependencyProperty PageHeaderTemplateProperty = DependencyProperty.Register("PageHeaderTemplate", typeof(DataTemplate), typeof(PrintableControlPreviewService), new PropertyMetadata(null));
        public static readonly DependencyProperty PageHeaderDataProperty = DependencyProperty.Register("PageHeaderData", typeof(object), typeof(PrintableControlPreviewService), new PropertyMetadata(null));
        public static readonly DependencyProperty PageFooterTemplateProperty = DependencyProperty.Register("PageFooterTemplate", typeof(DataTemplate), typeof(PrintableControlPreviewService), new PropertyMetadata(null));
        public static readonly DependencyProperty PageFooterDataProperty = DependencyProperty.Register("PageFooterData", typeof(object), typeof(PrintableControlPreviewService), new PropertyMetadata(null));
        public static readonly DependencyProperty PaperKindProperty = DependencyProperty.Register("PaperKind", typeof(System.Drawing.Printing.PaperKind), typeof(PrintableControlPreviewService), new PropertyMetadata(System.Drawing.Printing.PaperKind.Letter));

        protected virtual void ConfigurePreviewModel(LinkPreviewModel model)
        {
        }

        public IPreviewModelWrapper GetPreview()
        {
            PrintableControlLink link = new PrintableControlLink(base.AssociatedObject as IPrintableControl) {
                Landscape = this.Landscape,
                PageHeaderTemplate = this.PageHeaderTemplate,
                PageHeaderData = this.PageHeaderData,
                PageFooterTemplate = this.PageFooterTemplate,
                PageFooterData = this.PageFooterData,
                PaperKind = this.PaperKind
            };
            LinkPreviewModel model = new LinkPreviewModel(link);
            this.ConfigurePreviewModel(model);
            PreviewModelWrapper wrapper = new PreviewModelWrapper(model);
            link.CreateDocument(true);
            return wrapper;
        }

        public bool Landscape
        {
            get => 
                (bool) base.GetValue(LandscapeProperty);
            set => 
                base.SetValue(LandscapeProperty, value);
        }

        public DataTemplate PageHeaderTemplate
        {
            get => 
                (DataTemplate) base.GetValue(PageHeaderTemplateProperty);
            set => 
                base.SetValue(PageHeaderTemplateProperty, value);
        }

        public object PageHeaderData
        {
            get => 
                base.GetValue(PageHeaderDataProperty);
            set => 
                base.SetValue(PageHeaderDataProperty, value);
        }

        public DataTemplate PageFooterTemplate
        {
            get => 
                (DataTemplate) base.GetValue(PageFooterTemplateProperty);
            set => 
                base.SetValue(PageFooterTemplateProperty, value);
        }

        public object PageFooterData
        {
            get => 
                base.GetValue(PageFooterDataProperty);
            set => 
                base.SetValue(PageFooterDataProperty, value);
        }

        public System.Drawing.Printing.PaperKind PaperKind
        {
            get => 
                (System.Drawing.Printing.PaperKind) base.GetValue(PaperKindProperty);
            set => 
                base.SetValue(PaperKindProperty, value);
        }
    }
}

