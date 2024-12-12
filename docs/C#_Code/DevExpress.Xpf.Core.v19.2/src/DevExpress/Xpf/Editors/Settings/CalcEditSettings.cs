namespace DevExpress.Xpf.Editors.Settings
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Windows;

    public class CalcEditSettings : PopupBaseEditSettings
    {
        public static readonly DependencyProperty IsPopupAutoWidthProperty;
        public static readonly DependencyProperty PrecisionProperty;

        static CalcEditSettings()
        {
            Type forType = typeof(CalcEditSettings);
            TextEditSettings.MaskTypeProperty.OverrideMetadata(forType, new FrameworkPropertyMetadata(MaskType.Numeric));
            IsPopupAutoWidthProperty = DependencyPropertyManager.Register("IsPopupAutoWidth", typeof(bool), forType, new FrameworkPropertyMetadata(true));
            PrecisionProperty = DependencyPropertyManager.Register("Precision", typeof(int), forType, new FrameworkPropertyMetadata(6));
        }

        protected override void AssignToEditCore(IBaseEdit edit)
        {
            base.AssignToEditCore(edit);
            PopupCalcEdit calc = edit as PopupCalcEdit;
            if (calc != null)
            {
                base.SetValueFromSettings(IsPopupAutoWidthProperty, () => calc.IsPopupAutoWidth = this.IsPopupAutoWidth);
                base.SetValueFromSettings(PrecisionProperty, () => calc.Precision = this.Precision);
            }
        }

        public bool IsPopupAutoWidth
        {
            get => 
                (bool) base.GetValue(IsPopupAutoWidthProperty);
            set => 
                base.SetValue(IsPopupAutoWidthProperty, value);
        }

        public int Precision
        {
            get => 
                (int) base.GetValue(PrecisionProperty);
            set => 
                base.SetValue(PrecisionProperty, value);
        }
    }
}

