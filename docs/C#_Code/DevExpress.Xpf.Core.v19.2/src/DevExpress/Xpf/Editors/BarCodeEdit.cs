namespace DevExpress.Xpf.Editors
{
    using DevExpress.Utils;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors.Internal;
    using DevExpress.Xpf.Printing;
    using DevExpress.Xpf.Utils;
    using DevExpress.Xpf.Utils.About;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.BarCode;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    [LicenseProvider(typeof(DX_WPF_LicenseProvider)), DXToolboxBrowsable, ToolboxTabName("DX.19.2: Common Controls")]
    public class BarCodeEdit : BaseEdit, IFullBarCodeData, IBarCodeData, IImageExportSettings, IExportSettings
    {
        public static readonly DependencyProperty AutoModuleProperty;
        public static readonly DependencyProperty ModuleProperty;
        public static readonly DependencyProperty ShowTextProperty;
        public static readonly DependencyProperty VerticalTextAlignmentProperty;
        public static readonly DependencyProperty HorizontalTextAlignmentProperty;
        public static readonly DependencyProperty BinaryDataProperty;
        protected Grid gridRoot;
        protected BrickStyle brickStyle = new BrickStyle();

        static BarCodeEdit()
        {
            Type ownerType = typeof(BarCodeEdit);
            AutoModuleProperty = DependencyPropertyManager.Register("AutoModule", typeof(bool), ownerType, new PropertyMetadata(true, new PropertyChangedCallback(BarCodeEdit.InvalidateVisual)));
            ModuleProperty = DependencyPropertyManager.Register("Module", typeof(double), ownerType, new PropertyMetadata(2.0, new PropertyChangedCallback(BarCodeEdit.InvalidateVisual)));
            ShowTextProperty = DependencyPropertyManager.Register("ShowText", typeof(bool), ownerType, new PropertyMetadata(true, new PropertyChangedCallback(BarCodeEdit.InvalidateVisual)));
            VerticalTextAlignmentProperty = DependencyPropertyManager.Register("VerticalTextAlignment", typeof(VerticalAlignment), ownerType, new PropertyMetadata(VerticalAlignment.Bottom, new PropertyChangedCallback(BarCodeEdit.InvalidateVisual)));
            HorizontalTextAlignmentProperty = DependencyPropertyManager.Register("HorizontalTextAlignment", typeof(HorizontalAlignment), ownerType, new PropertyMetadata(HorizontalAlignment.Left, new PropertyChangedCallback(BarCodeEdit.InvalidateVisual)));
            BinaryDataProperty = DependencyPropertyManager.Register("BinaryData", typeof(byte[]), ownerType, new PropertyMetadata(new byte[0], new PropertyChangedCallback(BarCodeEdit.InvalidateVisual)));
            Control.HorizontalContentAlignmentProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(HorizontalAlignment.Left, new PropertyChangedCallback(BarCodeEdit.InvalidateStyle)));
            Control.VerticalContentAlignmentProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(VerticalAlignment.Top, new PropertyChangedCallback(BarCodeEdit.InvalidateStyle)));
            Control.ForegroundProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(new SolidColorBrush(Colors.Black), new PropertyChangedCallback(BarCodeEdit.InvalidateStyle)));
            BaseEdit.StyleSettingsProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(null, new PropertyChangedCallback(BarCodeEdit.UpdateSymbology)));
            BaseEdit.EditValueProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(null, new PropertyChangedCallback(BarCodeEdit.InvalidateVisual)));
        }

        public BarCodeEdit()
        {
            this.SetDefaultStyleKey(typeof(BarCodeEdit));
        }

        protected internal virtual void BarCodeInvalidateStyle()
        {
            DevExpress.XtraPrinting.TextAlignment fullAligment = AligmentHelper.GetFullAligment(this.VerticalTextAlignment, this.HorizontalTextAlignment);
            this.brickStyle.BackColor = WindowsFormsHelper.ConvertBrushToColor(base.Background);
            this.brickStyle.ForeColor = WindowsFormsHelper.ConvertBrushToColor(base.Foreground);
            this.brickStyle.StringFormat = BrickStringFormat.Create(fullAligment, true);
            this.brickStyle.TextAlignment = fullAligment;
            this.brickStyle.Padding = new PaddingInfo((int) base.Padding.Left, (int) base.Padding.Right, (int) base.Padding.Top, (int) base.Padding.Bottom, GraphicsDpi.Pixel);
            this.BarCodeInvalidateVisual();
        }

        protected internal virtual void BarCodeInvalidateVisual()
        {
            if (this.BarCodePainter != null)
            {
                this.BarCodePainter.InvalidateBarCodePainter();
            }
        }

        protected override ActualPropertyProvider CreateActualPropertyProvider() => 
            new BarCodePropertyProvider(this);

        protected override EditStrategyBase CreateEditStrategy() => 
            new BarCodeEditStrategy(this);

        protected internal override BaseEditStyleSettings CreateStyleSettings() => 
            new QRCodeStyleSettings();

        private static void InvalidateStyle(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((BarCodeEdit) d).BarCodeInvalidateStyle();
        }

        private static void InvalidateVisual(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((BarCodeEdit) d).BarCodeInvalidateVisual();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.BarCodePainter = (DevExpress.Xpf.Editors.Internal.BarCodePainter) base.GetTemplateChild("Part_BarCodePainter");
            this.gridRoot = (Grid) base.GetTemplateChild("Part_RootGrid");
            this.BarCodeInvalidateStyle();
        }

        protected internal virtual void UpdateSymbology(DependencyPropertyChangedEventArgs e)
        {
            BarCodeStyleSettings newValue = (BarCodeStyleSettings) e.NewValue;
            if (this.gridRoot != null)
            {
                if (e.OldValue is UIElement)
                {
                    this.gridRoot.Children.Remove(e.OldValue as UIElement);
                }
                if (newValue != null)
                {
                    this.gridRoot.Children.Add(newValue);
                }
            }
            if (newValue != null)
            {
                if (this.BarCodePainter != null)
                {
                    this.BarCodePainter.Symbology = newValue;
                }
                newValue.BarCodeEdit = this;
                this.BarCodeInvalidateVisual();
            }
        }

        private static void UpdateSymbology(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((BarCodeEdit) d).UpdateSymbology(e);
        }

        protected internal DevExpress.Xpf.Editors.Internal.BarCodePainter BarCodePainter { get; set; }

        public BarCodeStyleSettings StyleSettings
        {
            get => 
                (BarCodeStyleSettings) base.StyleSettings;
            set => 
                base.StyleSettings = value;
        }

        protected internal override Type StyleSettingsType =>
            typeof(BarCodeStyleSettings);

        private BarCodePropertyProvider PropertyProvider =>
            base.PropertyProvider as BarCodePropertyProvider;

        public bool AutoModule
        {
            get => 
                (bool) base.GetValue(AutoModuleProperty);
            set => 
                base.SetValue(AutoModuleProperty, value);
        }

        public double Module
        {
            get => 
                (double) base.GetValue(ModuleProperty);
            set => 
                base.SetValue(ModuleProperty, value);
        }

        public bool ShowText
        {
            get => 
                (bool) base.GetValue(ShowTextProperty);
            set => 
                base.SetValue(ShowTextProperty, value);
        }

        public VerticalAlignment VerticalTextAlignment
        {
            get => 
                (VerticalAlignment) base.GetValue(VerticalTextAlignmentProperty);
            set => 
                base.SetValue(VerticalTextAlignmentProperty, value);
        }

        public HorizontalAlignment HorizontalTextAlignment
        {
            get => 
                (HorizontalAlignment) base.GetValue(HorizontalTextAlignmentProperty);
            set => 
                base.SetValue(HorizontalTextAlignmentProperty, value);
        }

        public byte[] BinaryData
        {
            get => 
                (byte[]) base.GetValue(BinaryDataProperty);
            set => 
                base.SetValue(BinaryDataProperty, value);
        }

        DevExpress.XtraPrinting.TextAlignment IBarCodeData.Alignment =>
            AligmentHelper.GetFullAligment(base.VerticalContentAlignment, base.HorizontalContentAlignment);

        bool IBarCodeData.AutoModule =>
            this.AutoModule;

        double IBarCodeData.Module =>
            this.Module;

        BarCodeOrientation IBarCodeData.Orientation =>
            BarCodeOrientation.Normal;

        bool IBarCodeData.ShowText =>
            this.ShowText;

        BrickStyle IBarCodeData.Style =>
            this.brickStyle;

        string IBarCodeData.Text =>
            (base.EditValue != null) ? base.EditValue.ToString() : string.Empty;

        byte[] IFullBarCodeData.BinaryData =>
            this.BinaryData;

        FrameworkElement IImageExportSettings.SourceElement
        {
            get
            {
                RenderTargetBitmap bitmap = new RenderTargetBitmap((int) base.ActualWidth, (int) base.ActualHeight, 96.0, 96.0, PixelFormats.Pbgra32);
                DrawingVisual visual = new DrawingVisual();
                DrawingContext context = visual.RenderOpen();
                context.DrawRectangle(new VisualBrush(this), null, new Rect(0.0, 0.0, base.ActualWidth, base.ActualHeight));
                context.Close();
                bitmap.Render(visual);
                return new Image { Source = bitmap };
            }
        }

        ImageRenderMode IImageExportSettings.ImageRenderMode =>
            ImageRenderMode.UseImageSource;

        bool IImageExportSettings.ForceCenterImageMode =>
            false;

        object IImageExportSettings.ImageKey =>
            null;
    }
}

