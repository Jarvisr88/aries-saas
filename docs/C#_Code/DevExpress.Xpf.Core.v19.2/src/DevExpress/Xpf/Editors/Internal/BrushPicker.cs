namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    public class BrushPicker : Control, IBrushPicker
    {
        public static readonly DependencyProperty BrushProperty;
        public static readonly DependencyProperty BrushTypeProperty;
        public static readonly DependencyProperty EditModeProperty;
        private BrushEditingWrapper editingWrapper;

        public event EventHandler BrushChanged;

        static BrushPicker()
        {
            Type ownerType = typeof(BrushPicker);
            BrushProperty = DependencyPropertyManager.Register("Brush", typeof(object), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, (o, args) => ((BrushPicker) o).BrushPropertyChanged(args.NewValue)));
            BrushTypeProperty = DependencyPropertyManager.Register("BrushType", typeof(DevExpress.Xpf.Editors.BrushType), ownerType, new FrameworkPropertyMetadata(DevExpress.Xpf.Editors.BrushType.None, FrameworkPropertyMetadataOptions.AffectsMeasure, (o, args) => ((BrushPicker) o).BrushTypeChanged((DevExpress.Xpf.Editors.BrushType) args.NewValue)));
            EditModeProperty = DependencyPropertyManager.Register("EditMode", typeof(DevExpress.Xpf.Editors.EditMode), ownerType, new FrameworkPropertyMetadata(DevExpress.Xpf.Editors.EditMode.Standalone));
        }

        public BrushPicker()
        {
            this.SetDefaultStyleKey(typeof(BrushPicker));
            this.BrushEditing = BrushEditingWrapper.Create(this, DevExpress.Xpf.Editors.BrushType.None);
        }

        protected virtual void BrushPropertyChanged(object newValue)
        {
            this.BrushEditing.Do<BrushEditingWrapper>(x => x.SetBrush(newValue));
        }

        protected virtual void BrushTypeChanged(DevExpress.Xpf.Editors.BrushType newValue)
        {
        }

        public bool NeedsKey(Key key, ModifierKeys modifiers)
        {
            Func<bool> fallback = <>c.<>9__24_1;
            if (<>c.<>9__24_1 == null)
            {
                Func<bool> local1 = <>c.<>9__24_1;
                fallback = <>c.<>9__24_1 = () => true;
            }
            return this.BrushEditing.Return<BrushEditingWrapper, bool>(x => x.NeedsKey(key, modifiers), fallback);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (this.HasFocus)
            {
                base.Focus();
            }
            this.BrushEditing = BrushEditingWrapper.Create(this, this.BrushType);
            this.BrushEditing.FocusPickerIfNeeded();
            this.BrushEditing.SetBrush(this.Brush);
        }

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            base.OnGotFocus(e);
            Action<BrushEditingWrapper> action = <>c.<>9__29_0;
            if (<>c.<>9__29_0 == null)
            {
                Action<BrushEditingWrapper> local1 = <>c.<>9__29_0;
                action = <>c.<>9__29_0 = x => x.FocusPickerIfNeeded();
            }
            this.BrushEditing.Do<BrushEditingWrapper>(action);
        }

        public void PerformSync(object value)
        {
            this.Brush = this.BrushEditing.GetBrush(value);
            this.RaiseBrushChanged();
        }

        protected virtual void RaiseBrushChanged()
        {
            if (this.BrushChanged != null)
            {
                this.BrushChanged(this, EventArgs.Empty);
            }
        }

        public BrushEditingWrapper BrushEditing
        {
            get => 
                this.editingWrapper;
            set
            {
                if (!ReferenceEquals(this.editingWrapper, value))
                {
                    Action<BrushEditingWrapper> action = <>c.<>9__9_0;
                    if (<>c.<>9__9_0 == null)
                    {
                        Action<BrushEditingWrapper> local1 = <>c.<>9__9_0;
                        action = <>c.<>9__9_0 = x => x.Unsubscribe();
                    }
                    this.editingWrapper.Do<BrushEditingWrapper>(action);
                    this.editingWrapper = value;
                    Action<BrushEditingWrapper> action2 = <>c.<>9__9_1;
                    if (<>c.<>9__9_1 == null)
                    {
                        Action<BrushEditingWrapper> local2 = <>c.<>9__9_1;
                        action2 = <>c.<>9__9_1 = x => x.Subscribe();
                    }
                    this.editingWrapper.Do<BrushEditingWrapper>(action2);
                }
            }
        }

        public bool HasFocus =>
            base.IsKeyboardFocusWithin;

        public DevExpress.Xpf.Editors.EditMode EditMode
        {
            get => 
                (DevExpress.Xpf.Editors.EditMode) base.GetValue(EditModeProperty);
            set => 
                base.SetValue(EditModeProperty, value);
        }

        public object Brush
        {
            get => 
                base.GetValue(BrushProperty);
            set => 
                base.SetValue(BrushProperty, value);
        }

        public DevExpress.Xpf.Editors.BrushType BrushType
        {
            get => 
                (DevExpress.Xpf.Editors.BrushType) base.GetValue(BrushTypeProperty);
            set => 
                base.SetValue(BrushTypeProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BrushPicker.<>c <>9 = new BrushPicker.<>c();
            public static Action<BrushEditingWrapper> <>9__9_0;
            public static Action<BrushEditingWrapper> <>9__9_1;
            public static Func<bool> <>9__24_1;
            public static Action<BrushEditingWrapper> <>9__29_0;

            internal void <.cctor>b__3_0(DependencyObject o, DependencyPropertyChangedEventArgs args)
            {
                ((BrushPicker) o).BrushPropertyChanged(args.NewValue);
            }

            internal void <.cctor>b__3_1(DependencyObject o, DependencyPropertyChangedEventArgs args)
            {
                ((BrushPicker) o).BrushTypeChanged((BrushType) args.NewValue);
            }

            internal bool <NeedsKey>b__24_1() => 
                true;

            internal void <OnGotFocus>b__29_0(BrushEditingWrapper x)
            {
                x.FocusPickerIfNeeded();
            }

            internal void <set_BrushEditing>b__9_0(BrushEditingWrapper x)
            {
                x.Unsubscribe();
            }

            internal void <set_BrushEditing>b__9_1(BrushEditingWrapper x)
            {
                x.Subscribe();
            }
        }
    }
}

