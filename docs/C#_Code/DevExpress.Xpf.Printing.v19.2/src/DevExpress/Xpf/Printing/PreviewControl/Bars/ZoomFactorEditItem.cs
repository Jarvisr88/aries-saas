namespace DevExpress.Xpf.Printing.PreviewControl.Bars
{
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.DocumentViewer;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.Windows;

    public class ZoomFactorEditItem : BarEditItem
    {
        public static readonly DependencyProperty SettingsSourceProperty = DependencyProperty.Register("SettingsSource", typeof(DocumentViewerControl), typeof(ZoomFactorEditItem));

        public ZoomFactorEditItem()
        {
            base.DefaultStyleKey = typeof(ZoomFactorEditItem);
            TrackBarEditSettings settings1 = new TrackBarEditSettings();
            settings1.Minimum = 0.0;
            settings1.Maximum = 2.0;
            settings1.SmallStep = 0.05;
            settings1.LargeStep = 0.1;
            settings1.TickFrequency = 2.0;
            settings1.StyleSettings = new TrackBarZoomStyleSettings();
            base.EditSettings = settings1;
            base.EditWidth = 150.0;
        }

        public DocumentViewerControl SettingsSource
        {
            get => 
                base.GetValue(SettingsSourceProperty) as DocumentViewerControl;
            set => 
                base.SetValue(SettingsSourceProperty, value);
        }
    }
}

