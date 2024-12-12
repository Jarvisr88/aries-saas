namespace DevExpress.Xpf.Editors.Settings
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Windows;

    public class BarCodeEditSettings : BaseEditSettings
    {
        public static readonly DependencyProperty AutoModuleProperty;
        public static readonly DependencyProperty ModuleProperty;
        public static readonly DependencyProperty ShowTextProperty;
        public static readonly DependencyProperty VerticalTextAlignmentProperty;
        public static readonly DependencyProperty HorizontalTextAlignmentProperty;
        public static readonly DependencyProperty BinaryDataProperty;

        static BarCodeEditSettings()
        {
            Type ownerType = typeof(BarCodeEditSettings);
            AutoModuleProperty = DependencyPropertyManager.Register("AutoModule", typeof(bool), ownerType, new PropertyMetadata(true, new PropertyChangedCallback(BarCodeEditSettings.InvalidateVisual)));
            ModuleProperty = DependencyPropertyManager.Register("Module", typeof(double), ownerType, new PropertyMetadata(2.0, new PropertyChangedCallback(BarCodeEditSettings.InvalidateVisual)));
            ShowTextProperty = DependencyPropertyManager.Register("ShowText", typeof(bool), ownerType, new PropertyMetadata(true, new PropertyChangedCallback(BarCodeEditSettings.InvalidateVisual)));
            VerticalTextAlignmentProperty = DependencyPropertyManager.Register("VerticalTextAlignment", typeof(VerticalAlignment), ownerType, new PropertyMetadata(VerticalAlignment.Bottom, new PropertyChangedCallback(BarCodeEditSettings.InvalidateVisual)));
            HorizontalTextAlignmentProperty = DependencyPropertyManager.Register("HorizontalTextAlignment", typeof(HorizontalAlignment), ownerType, new PropertyMetadata(HorizontalAlignment.Left, new PropertyChangedCallback(BarCodeEditSettings.InvalidateVisual)));
            BinaryDataProperty = DependencyPropertyManager.Register("BinaryData", typeof(byte[]), ownerType, new PropertyMetadata(new byte[0], new PropertyChangedCallback(BarCodeEditSettings.UpdateBarCodeGenerator)));
            BaseEditSettings.HorizontalContentAlignmentProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(EditSettingsHorizontalAlignment.Left, new PropertyChangedCallback(BarCodeEditSettings.InvalidateStyle)));
            BaseEditSettings.VerticalContentAlignmentProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(VerticalAlignment.Top, new PropertyChangedCallback(BarCodeEditSettings.InvalidateStyle)));
            BaseEditSettings.StyleSettingsProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(null, new PropertyChangedCallback(BarCodeEditSettings.UpdateSymbology)));
        }

        protected override void AssignToEditCore(IBaseEdit edit)
        {
            base.AssignToEditCore(edit);
            BarCodeEdit editor = edit as BarCodeEdit;
            if (editor != null)
            {
                base.SetValueFromSettings(AutoModuleProperty, () => editor.AutoModule = this.AutoModule);
                base.SetValueFromSettings(ModuleProperty, () => editor.Module = this.Module);
                base.SetValueFromSettings(ShowTextProperty, () => editor.ShowText = this.ShowText);
                base.SetValueFromSettings(VerticalTextAlignmentProperty, () => editor.VerticalTextAlignment = this.VerticalTextAlignment);
                base.SetValueFromSettings(HorizontalTextAlignmentProperty, () => editor.HorizontalTextAlignment = this.HorizontalTextAlignment);
                base.SetValueFromSettings(BinaryDataProperty, () => editor.BinaryData = this.BinaryData);
            }
        }

        private static void InvalidateStyle(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        private static void InvalidateVisual(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        private static void UpdateBarCodeGenerator(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        private static void UpdateSymbology(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

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
    }
}

