namespace DevExpress.Xpf.Core.ConditionalFormatting
{
    using DevExpress.Utils.Serializing;
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Media;

    public class Format : Freezable
    {
        private bool forcedDisableTextDecoration;
        private TextDecorationCollection defaultTextDecoration;
        public static readonly DependencyProperty BackgroundProperty = DependencyProperty.Register("Background", typeof(Brush), typeof(Format), CreateMetadata(ConditionalFormatMask.Background));
        public static readonly DependencyProperty ForegroundProperty = DependencyProperty.Register("Foreground", typeof(Brush), typeof(Format), CreateMetadata(ConditionalFormatMask.Foreground));
        public static readonly DependencyProperty FontSizeProperty = DependencyProperty.Register("FontSize", typeof(double), typeof(Format), CreateMetadata(ConditionalFormatMask.FontSize));
        public static readonly DependencyProperty FontStyleProperty = DependencyProperty.Register("FontStyle", typeof(System.Windows.FontStyle), typeof(Format), CreateMetadata(ConditionalFormatMask.FontStyle));
        public static readonly DependencyProperty FontFamilyProperty = DependencyProperty.Register("FontFamily", typeof(System.Windows.Media.FontFamily), typeof(Format), CreateMetadata(ConditionalFormatMask.FontFamily));
        public static readonly DependencyProperty FontStretchProperty = DependencyProperty.Register("FontStretch", typeof(System.Windows.FontStretch), typeof(Format), CreateMetadata(ConditionalFormatMask.FontStretch));
        public static readonly DependencyProperty FontWeightProperty = DependencyProperty.Register("FontWeight", typeof(System.Windows.FontWeight), typeof(Format), CreateMetadata(ConditionalFormatMask.FontWeight));
        public static readonly DependencyProperty TextDecorationsProperty = DependencyProperty.Register("TextDecorations", typeof(TextDecorationCollection), typeof(Format), CreateMetadata(ConditionalFormatMask.TextDecorations));
        public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(ImageSource), typeof(Format), CreateMetadata(ConditionalFormatMask.DataBarOrIcon));
        public static readonly DependencyProperty IconVerticalAlignmentProperty = DependencyProperty.Register("IconVerticalAlignment", typeof(VerticalAlignment), typeof(Format), new PropertyMetadata(VerticalAlignment.Center));

        public Format()
        {
            this.FormatMask = ConditionalFormatMask.None;
            this.defaultTextDecoration = new TextDecorationCollection();
            this.TextDecorations = this.defaultTextDecoration;
        }

        protected override Freezable CreateInstanceCore() => 
            new Format { forcedDisableTextDecoration = !this.FormatMask.HasFlag(ConditionalFormatMask.TextDecorations) };

        private static PropertyMetadata CreateMetadata(ConditionalFormatMask flag) => 
            new PropertyMetadata(delegate (DependencyObject d, DependencyPropertyChangedEventArgs e) {
                ((Format) d).UpdateFormatMask(e, flag);
            });

        private bool IsTextDecorationsPropertyAssigned() => 
            !this.forcedDisableTextDecoration && ((this.TextDecorations != null) && (!ReferenceEquals(this.TextDecorations, this.defaultTextDecoration) || (this.TextDecorations.Count != 0)));

        private void UpdateFormatMask(DependencyPropertyChangedEventArgs e, ConditionalFormatMask flag)
        {
            if (ReferenceEquals(e.Property, TextDecorationsProperty) ? this.IsTextDecorationsPropertyAssigned() : this.IsPropertyAssigned(e.Property))
            {
                this.FormatMask |= flag;
            }
            else
            {
                this.FormatMask &= ~flag;
            }
        }

        private bool XtraShouldSerializeTextDecorations() => 
            this.IsTextDecorationsPropertyAssigned();

        [XtraSerializableProperty]
        public Brush Background
        {
            get => 
                (Brush) base.GetValue(BackgroundProperty);
            set => 
                base.SetValue(BackgroundProperty, value);
        }

        [XtraSerializableProperty]
        public Brush Foreground
        {
            get => 
                (Brush) base.GetValue(ForegroundProperty);
            set => 
                base.SetValue(ForegroundProperty, value);
        }

        [XtraSerializableProperty]
        public double FontSize
        {
            get => 
                (double) base.GetValue(FontSizeProperty);
            set => 
                base.SetValue(FontSizeProperty, value);
        }

        [XtraSerializableProperty]
        public System.Windows.FontStyle FontStyle
        {
            get => 
                (System.Windows.FontStyle) base.GetValue(FontStyleProperty);
            set => 
                base.SetValue(FontStyleProperty, value);
        }

        [XtraSerializableProperty]
        public System.Windows.Media.FontFamily FontFamily
        {
            get => 
                (System.Windows.Media.FontFamily) base.GetValue(FontFamilyProperty);
            set => 
                base.SetValue(FontFamilyProperty, value);
        }

        [XtraSerializableProperty]
        public System.Windows.FontStretch FontStretch
        {
            get => 
                (System.Windows.FontStretch) base.GetValue(FontStretchProperty);
            set => 
                base.SetValue(FontStretchProperty, value);
        }

        [XtraSerializableProperty]
        public System.Windows.FontWeight FontWeight
        {
            get => 
                (System.Windows.FontWeight) base.GetValue(FontWeightProperty);
            set => 
                base.SetValue(FontWeightProperty, value);
        }

        [XtraSerializableProperty]
        public TextDecorationCollection TextDecorations
        {
            get => 
                (TextDecorationCollection) base.GetValue(TextDecorationsProperty);
            set => 
                base.SetValue(TextDecorationsProperty, value);
        }

        [XtraSerializableProperty]
        public ImageSource Icon
        {
            get => 
                (ImageSource) base.GetValue(IconProperty);
            set => 
                base.SetValue(IconProperty, value);
        }

        [XtraSerializableProperty]
        public VerticalAlignment IconVerticalAlignment
        {
            get => 
                (VerticalAlignment) base.GetValue(IconVerticalAlignmentProperty);
            set => 
                base.SetValue(IconVerticalAlignmentProperty, value);
        }

        internal ConditionalFormatMask FormatMask { get; private set; }
    }
}

