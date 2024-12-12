namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.Internal;
    using DevExpress.XtraPrinting.BarCode;
    using System;
    using System.ComponentModel;
    using System.Windows;

    public abstract class Code128BarCodeStyleSettings<T> : BarCodeStyleSettings<T> where T: Code128Generator, new()
    {
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty CharacterSetProperty;

        static Code128BarCodeStyleSettings()
        {
            Code128BarCodeStyleSettings<T>.CharacterSetProperty = DependencyProperty.Register("CharacterSet", typeof(Code128Charset), typeof(Code128BarCodeStyleSettings<T>), new PropertyMetadata(Code128Charset.CharsetA, new PropertyChangedCallback(Code128BarCodeStyleSettings<T>.CharacterSetPropertyChanged)));
        }

        protected Code128BarCodeStyleSettings()
        {
        }

        public override void ApplyToEdit(BaseEdit editor)
        {
            base.ApplyToEdit(editor);
            ((BarCodePropertyProvider) editor.PropertyProvider).CharacterSet = this.CharacterSet;
        }

        private static void CharacterSetPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Code128BarCodeStyleSettings<T>) d).Generator.CharacterSet = (Code128Charset) e.NewValue;
            BarCodeControlInvalidateVisual(d);
        }

        [TypeConverter(typeof(XamlEnumTypeConverter<Code128Charset>))]
        public Code128Charset CharacterSet
        {
            get => 
                (Code128Charset) base.GetValue(Code128BarCodeStyleSettings<T>.CharacterSetProperty);
            set => 
                base.SetValue(Code128BarCodeStyleSettings<T>.CharacterSetProperty, value);
        }
    }
}

