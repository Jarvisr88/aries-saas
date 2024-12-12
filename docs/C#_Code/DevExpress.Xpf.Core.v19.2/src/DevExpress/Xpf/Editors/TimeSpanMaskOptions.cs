namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class TimeSpanMaskOptions : DependencyObject
    {
        public static readonly DependencyProperty AssignValueToEnteredLiteralProperty;
        public static readonly DependencyProperty ChangeNextPartOnCycleValueChangeProperty;
        public static readonly DependencyProperty InputModeProperty;
        public static readonly DependencyProperty DefaultPartProperty;
        public static readonly DependencyProperty AllowNegativeValueProperty;

        static TimeSpanMaskOptions()
        {
            AssignValueToEnteredLiteralProperty = DependencyProperty.RegisterAttached("AssignValueToEnteredLiteral", typeof(bool), typeof(TimeSpanMaskOptions), new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.None, (d, args) => UpdateEditorMask(d)));
            ChangeNextPartOnCycleValueChangeProperty = DependencyProperty.RegisterAttached("ChangeNextPartOnCycleValueChange", typeof(bool), typeof(TimeSpanMaskOptions), new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.None, (d, args) => UpdateEditorMask(d)));
            InputModeProperty = DependencyProperty.RegisterAttached("InputMode", typeof(TimeSpanInputMode), typeof(TimeSpanMaskOptions), new FrameworkPropertyMetadata(TimeSpanInputMode.NotRestrictedLargestUnit, FrameworkPropertyMetadataOptions.None, (d, args) => UpdateEditorMask(d)));
            DefaultPartProperty = DependencyProperty.RegisterAttached("DefaultPart", typeof(TimeSpanPart), typeof(TimeSpanMaskOptions), new FrameworkPropertyMetadata(TimeSpanPart.Hours, FrameworkPropertyMetadataOptions.None, (d, args) => UpdateEditorMask(d)));
            AllowNegativeValueProperty = DependencyProperty.RegisterAttached("AllowNegativeValue", typeof(bool), typeof(TimeSpanMaskOptions), new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.None, (d, args) => UpdateEditorMask(d)));
        }

        public static bool GetAllowNegativeValue(DependencyObject d) => 
            (bool) d.GetValue(AllowNegativeValueProperty);

        public static bool GetAssignValueToEnteredLiteral(DependencyObject d) => 
            (bool) d.GetValue(AssignValueToEnteredLiteralProperty);

        public static bool GetChangeNextPartOnCycleValueChange(DependencyObject d) => 
            (bool) d.GetValue(ChangeNextPartOnCycleValueChangeProperty);

        public static TimeSpanPart GetDefaultPart(DependencyObject d) => 
            (TimeSpanPart) d.GetValue(DefaultPartProperty);

        public static TimeSpanInputMode GetInputMode(DependencyObject d) => 
            (TimeSpanInputMode) d.GetValue(InputModeProperty);

        public static void SetAllowNegativeValue(DependencyObject d, bool value)
        {
            d.SetValue(AllowNegativeValueProperty, value);
        }

        public static void SetAssignValueToEnteredLiteral(DependencyObject d, bool value)
        {
            d.SetValue(AssignValueToEnteredLiteralProperty, value);
        }

        public static void SetChangeNextPartOnCycleValueChange(DependencyObject d, bool value)
        {
            d.SetValue(ChangeNextPartOnCycleValueChangeProperty, value);
        }

        public static void SetDefaultPart(DependencyObject d, TimeSpanPart value)
        {
            d.SetValue(DefaultPartProperty, value);
        }

        public static void SetInputMode(DependencyObject d, TimeSpanInputMode value)
        {
            d.SetValue(InputModeProperty, value);
        }

        private static void UpdateEditorMask(DependencyObject d)
        {
            TextEdit edit = d as TextEdit;
            if (edit != null)
            {
                edit.MaskPropertiesChanged();
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TimeSpanMaskOptions.<>c <>9 = new TimeSpanMaskOptions.<>c();

            internal void <.cctor>b__17_0(DependencyObject d, DependencyPropertyChangedEventArgs args)
            {
                TimeSpanMaskOptions.UpdateEditorMask(d);
            }

            internal void <.cctor>b__17_1(DependencyObject d, DependencyPropertyChangedEventArgs args)
            {
                TimeSpanMaskOptions.UpdateEditorMask(d);
            }

            internal void <.cctor>b__17_2(DependencyObject d, DependencyPropertyChangedEventArgs args)
            {
                TimeSpanMaskOptions.UpdateEditorMask(d);
            }

            internal void <.cctor>b__17_3(DependencyObject d, DependencyPropertyChangedEventArgs args)
            {
                TimeSpanMaskOptions.UpdateEditorMask(d);
            }

            internal void <.cctor>b__17_4(DependencyObject d, DependencyPropertyChangedEventArgs args)
            {
                TimeSpanMaskOptions.UpdateEditorMask(d);
            }
        }
    }
}

