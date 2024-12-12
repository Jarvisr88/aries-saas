namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.DocumentViewer;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.PdfViewer.Themes;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;

    public class PdfBarCheckItem : BarCheckItem
    {
        public static readonly DependencyProperty PdfBarItemNameProperty;

        static PdfBarCheckItem()
        {
            PdfBarItemNameProperty = DependencyPropertyManager.Register("PdfBarItemName", typeof(string), typeof(PdfBarCheckItem), new PropertyMetadata(string.Empty, (obj, args) => ((PdfBarCheckItem) obj).OnPdfBarItemNameChanged((string) args.NewValue)));
        }

        protected override void OnCommandChanged(ICommand oldCommand, ICommand newCommand)
        {
            base.OnCommandChanged(oldCommand, newCommand);
            CommandBase base2 = newCommand as CommandBase;
            if (base2 != null)
            {
                base.DataContext = base2;
                PdfViewerThemeKeyExtension resourceKey = new PdfViewerThemeKeyExtension();
                resourceKey.ResourceKey = PdfViewerThemeKeys.BarCheckItemStyle;
                resourceKey.ThemeName = ThemeHelper.GetEditorThemeName(this);
                base.Style = (Style) base.FindResource(resourceKey);
            }
        }

        protected virtual void OnPdfBarItemNameChanged(string newValue)
        {
            if (!this.IsPropertySet(FrameworkContentElement.NameProperty))
            {
                base.Name = newValue;
            }
        }

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
            public static readonly PdfBarCheckItem.<>c <>9 = new PdfBarCheckItem.<>c();

            internal void <.cctor>b__7_0(DependencyObject obj, DependencyPropertyChangedEventArgs args)
            {
                ((PdfBarCheckItem) obj).OnPdfBarItemNameChanged((string) args.NewValue);
            }
        }
    }
}

