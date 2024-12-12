namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class TimeSpanMaskGanttOptions : DependencyObject
    {
        public static readonly DependencyProperty DayDurationProperty;

        static TimeSpanMaskGanttOptions()
        {
            DayDurationProperty = DependencyProperty.RegisterAttached("DayDuration", typeof(TimeSpan?), typeof(TimeSpanMaskGanttOptions), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits, (d, args) => UpdateEditorMask(d)));
        }

        public static TimeSpan? GetDayDuration(DependencyObject d) => 
            (TimeSpan?) d.GetValue(DayDurationProperty);

        public static void SetDayDuration(DependencyObject d, TimeSpan? value)
        {
            d.SetValue(DayDurationProperty, value);
        }

        private static void UpdateEditorMask(DependencyObject d)
        {
            Action<ISupportRaiseChanged> action = <>c.<>9__3_0;
            if (<>c.<>9__3_0 == null)
            {
                Action<ISupportRaiseChanged> local1 = <>c.<>9__3_0;
                action = <>c.<>9__3_0 = x => x.RaiseChanged();
            }
            (d as ISupportRaiseChanged).Do<ISupportRaiseChanged>(action);
            Action<TextEdit> action2 = <>c.<>9__3_1;
            if (<>c.<>9__3_1 == null)
            {
                Action<TextEdit> local2 = <>c.<>9__3_1;
                action2 = <>c.<>9__3_1 = x => x.MaskPropertiesChanged();
            }
            (d as TextEdit).Do<TextEdit>(action2);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TimeSpanMaskGanttOptions.<>c <>9 = new TimeSpanMaskGanttOptions.<>c();
            public static Action<ISupportRaiseChanged> <>9__3_0;
            public static Action<TextEdit> <>9__3_1;

            internal void <.cctor>b__5_0(DependencyObject d, DependencyPropertyChangedEventArgs args)
            {
                TimeSpanMaskGanttOptions.UpdateEditorMask(d);
            }

            internal void <UpdateEditorMask>b__3_0(ISupportRaiseChanged x)
            {
                x.RaiseChanged();
            }

            internal void <UpdateEditorMask>b__3_1(TextEdit x)
            {
                x.MaskPropertiesChanged();
            }
        }
    }
}

