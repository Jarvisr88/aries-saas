namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.PdfViewer.Themes;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    public class PdfBarCommentItem : BarSplitCheckItem
    {
        public static readonly DependencyProperty PdfBarItemNameProperty;

        static PdfBarCommentItem()
        {
            PdfBarItemNameProperty = DependencyPropertyManager.Register("PdfBarItemName", typeof(string), typeof(PdfBarCommentItem), new PropertyMetadata(string.Empty, (obj, args) => ((PdfBarCommentItem) obj).OnPdfBarItemNameChanged((string) args.NewValue)));
        }

        public PdfBarCommentItem()
        {
            PopupControlContainer container1 = new PopupControlContainer();
            container1.Content = new ContentControl();
            base.PopupControl = container1;
        }

        protected override void OnCommandChanged(ICommand oldCommand, ICommand newCommand)
        {
            base.OnCommandChanged(oldCommand, newCommand);
            this.PopupContainer.Content.ClearValue(ContentControl.ContentTemplateProperty);
            if (newCommand != null)
            {
                PdfViewerThemeKeyExtension resourceKey = new PdfViewerThemeKeyExtension();
                resourceKey.ResourceKey = PdfViewerThemeKeys.PdfBarCommentItemContentTemplate;
                resourceKey.ThemeName = ThemeHelper.GetEditorThemeName(this);
                ((ContentControl) this.PopupContainer.Content).ContentTemplate = (DataTemplate) base.FindResource(resourceKey);
                ((ContentControl) this.PopupContainer.Content).Content = newCommand;
                base.DataContext = newCommand;
                PdfViewerThemeKeyExtension extension2 = new PdfViewerThemeKeyExtension();
                extension2.ResourceKey = PdfViewerThemeKeys.PdfBarCommentItemStyle;
                extension2.ThemeName = ThemeHelper.GetEditorThemeName(this);
                base.Style = (Style) base.FindResource(extension2);
            }
        }

        protected virtual void OnPdfBarItemNameChanged(string newValue)
        {
            if (!this.IsPropertySet(FrameworkContentElement.NameProperty))
            {
                base.Name = newValue;
            }
        }

        private PopupControlContainer PopupContainer =>
            base.PopupControl as PopupControlContainer;

        public string PdfBarItemName
        {
            get => 
                (string) base.GetValue(PdfBarItemNameProperty);
            set => 
                base.SetValue(PdfBarItemNameProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PdfBarCommentItem.<>c <>9 = new PdfBarCommentItem.<>c();

            internal void <.cctor>b__9_0(DependencyObject obj, DependencyPropertyChangedEventArgs args)
            {
                ((PdfBarCommentItem) obj).OnPdfBarItemNameChanged((string) args.NewValue);
            }
        }
    }
}

