namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors.Helpers;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;

    public class SolidColorBrushWrapper : BrushEditingWrapper
    {
        private readonly Locker syncBrushLocker;

        public SolidColorBrushWrapper(IBrushPicker editor) : base(editor)
        {
            this.syncBrushLocker = new Locker();
        }

        public override System.Windows.Media.Brush GetBrush(object baseValue) => 
            baseValue.ToSolidColorBrush();

        public override bool NeedsKey(Key key, ModifierKeys modifiers)
        {
            Func<bool> fallback = <>c.<>9__11_1;
            if (<>c.<>9__11_1 == null)
            {
                Func<bool> local1 = <>c.<>9__11_1;
                fallback = <>c.<>9__11_1 = () => false;
            }
            return this.ColorPicker.Return<DevExpress.Xpf.Editors.Internal.ColorPicker, bool>(x => x.NeedsKey(key, modifiers), fallback);
        }

        protected virtual void PickerColorChanged(object sender, RoutedEventArgs e)
        {
            ColorChangedEventArgs colorArgs = (ColorChangedEventArgs) e;
            this.syncBrushLocker.DoIfNotLocked(delegate {
                Action<IBrushPicker> <>9__1;
                Action<IBrushPicker> action = <>9__1;
                if (<>9__1 == null)
                {
                    Action<IBrushPicker> local1 = <>9__1;
                    action = <>9__1 = x => x.PerformSync(colorArgs.Color);
                }
                this.Picker.Do<IBrushPicker>(action);
            });
        }

        public override void SetBrush(object editValue)
        {
            base.SetBrush(editValue);
            this.syncBrushLocker.DoLockedAction<DevExpress.Xpf.Editors.Internal.ColorPicker>(delegate {
                Action<DevExpress.Xpf.Editors.Internal.ColorPicker> <>9__1;
                Action<DevExpress.Xpf.Editors.Internal.ColorPicker> action = <>9__1;
                if (<>9__1 == null)
                {
                    Action<DevExpress.Xpf.Editors.Internal.ColorPicker> local1 = <>9__1;
                    action = <>9__1 = x => x.Color = this.GetBrush(editValue).ToSolidColorBrush().Color;
                }
                return this.ColorPicker.Do<DevExpress.Xpf.Editors.Internal.ColorPicker>(action);
            });
        }

        public override void Subscribe()
        {
            this.ColorPicker.Do<DevExpress.Xpf.Editors.Internal.ColorPicker>(delegate (DevExpress.Xpf.Editors.Internal.ColorPicker x) {
                x.ColorChanged += new RoutedEventHandler(this.PickerColorChanged);
            });
        }

        public override void Unsubscribe()
        {
            this.ColorPicker.Do<DevExpress.Xpf.Editors.Internal.ColorPicker>(delegate (DevExpress.Xpf.Editors.Internal.ColorPicker x) {
                x.ColorChanged -= new RoutedEventHandler(this.PickerColorChanged);
            });
        }

        private DevExpress.Xpf.Editors.Internal.ColorPicker ColorPicker =>
            base.ContentElement as DevExpress.Xpf.Editors.Internal.ColorPicker;

        public override System.Windows.Media.Brush Brush
        {
            get
            {
                Func<DevExpress.Xpf.Editors.Internal.ColorPicker, SolidColorBrush> evaluator = <>c.<>9__5_0;
                if (<>c.<>9__5_0 == null)
                {
                    Func<DevExpress.Xpf.Editors.Internal.ColorPicker, SolidColorBrush> local1 = <>c.<>9__5_0;
                    evaluator = <>c.<>9__5_0 = x => new SolidColorBrush(x.Color);
                }
                return this.ColorPicker.Return<DevExpress.Xpf.Editors.Internal.ColorPicker, SolidColorBrush>(evaluator, (<>c.<>9__5_1 ??= () => new SolidColorBrush(Text2ColorHelper.DefaultColor)));
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SolidColorBrushWrapper.<>c <>9 = new SolidColorBrushWrapper.<>c();
            public static Func<ColorPicker, SolidColorBrush> <>9__5_0;
            public static Func<SolidColorBrush> <>9__5_1;
            public static Func<bool> <>9__11_1;

            internal SolidColorBrush <get_Brush>b__5_0(ColorPicker x) => 
                new SolidColorBrush(x.Color);

            internal SolidColorBrush <get_Brush>b__5_1() => 
                new SolidColorBrush(Text2ColorHelper.DefaultColor);

            internal bool <NeedsKey>b__11_1() => 
                false;
        }
    }
}

