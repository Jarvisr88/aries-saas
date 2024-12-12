namespace DevExpress.Xpf.Printing.PreviewControl.Bars
{
    using DevExpress.Mvvm.UI.Interactivity;
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.DocumentViewer;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Settings;
    using DevExpress.Xpf.Printing;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class PageNumberEditItem : BarEditItem
    {
        public static readonly DependencyProperty SettingsSourceProperty;
        private PageNumberBehavior behavior;

        static PageNumberEditItem()
        {
            SettingsSourceProperty = DependencyProperty.Register("SettingsSource", typeof(DocumentViewerControl), typeof(PageNumberEditItem), new FrameworkPropertyMetadata(null, (d, e) => ((PageNumberEditItem) d).OnSourceChanged()));
        }

        public PageNumberEditItem()
        {
            base.DefaultStyleKey = typeof(PageNumberEditItem);
            TextEditSettings settings1 = new TextEditSettings();
            settings1.HorizontalContentAlignment = EditSettingsHorizontalAlignment.Center;
            settings1.MaskType = MaskType.RegEx;
            settings1.Mask = @"\d{0,6}";
            settings1.AllowNullInput = true;
            base.EditSettings = settings1;
            this.behavior = new PageNumberBehavior();
            Interaction.GetBehaviors(this).Add(this.behavior);
        }

        private void OnSourceChanged()
        {
            this.behavior.FocusTarget = this.SettingsSource;
        }

        public DocumentViewerControl SettingsSource
        {
            get => 
                base.GetValue(SettingsSourceProperty) as DocumentViewerControl;
            set => 
                base.SetValue(SettingsSourceProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PageNumberEditItem.<>c <>9 = new PageNumberEditItem.<>c();

            internal void <.cctor>b__2_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((PageNumberEditItem) d).OnSourceChanged();
            }
        }
    }
}

