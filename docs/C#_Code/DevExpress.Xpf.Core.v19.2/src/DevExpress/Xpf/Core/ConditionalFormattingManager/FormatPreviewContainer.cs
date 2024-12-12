namespace DevExpress.Xpf.Core.ConditionalFormattingManager
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class FormatPreviewContainer : ManagerContainerBase
    {
        public static readonly DependencyProperty FormatProperty;
        private ContentControl previewControl;

        static FormatPreviewContainer()
        {
            FormatProperty = DependencyProperty.Register("Format", typeof(Freezable), typeof(FormatPreviewContainer), new PropertyMetadata(null, (d, e) => ((FormatPreviewContainer) d).UpdatePreview()));
        }

        public FormatPreviewContainer()
        {
            this.SetDefaultStyleKey(typeof(FormatPreviewContainer));
        }

        protected override object CreateContent()
        {
            this.previewControl = GridAssemblyHelper.Instance.CreatePreviewControl();
            this.UpdatePreview();
            return this.previewControl;
        }

        private void UpdatePreview()
        {
            this.previewControl.Do<ContentControl>(x => x.Content = this.Format);
        }

        public Freezable Format
        {
            get => 
                (Freezable) base.GetValue(FormatProperty);
            set => 
                base.SetValue(FormatProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FormatPreviewContainer.<>c <>9 = new FormatPreviewContainer.<>c();

            internal void <.cctor>b__8_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((FormatPreviewContainer) d).UpdatePreview();
            }
        }
    }
}

