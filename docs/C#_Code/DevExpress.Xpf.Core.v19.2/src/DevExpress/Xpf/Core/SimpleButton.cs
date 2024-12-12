namespace DevExpress.Xpf.Core
{
    using DevExpress.Utils;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;
    using System.Windows.Markup;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Threading;

    [DXToolboxBrowsable(DXToolboxItemKind.Free), ToolboxTabName("DX.19.2: Common Controls")]
    public class SimpleButton : Button
    {
        public static readonly DependencyProperty GlyphProperty = DependencyPropertyManager.Register("Glyph", typeof(ImageSource), typeof(SimpleButton), new FrameworkPropertyMetadata(null, null, (d, e) => ((SimpleButton) d).OnGlyphChanging(e)));
        public static readonly DependencyProperty GlyphToContentOffsetProperty = DependencyPropertyManager.Register("GlyphToContentOffset", typeof(double), typeof(SimpleButton), new FrameworkPropertyMetadata(0.0));
        public static readonly DependencyProperty GlyphHeightProperty = DependencyPropertyManager.Register("GlyphHeight", typeof(double?), typeof(SimpleButton), new FrameworkPropertyMetadata(null));
        public static readonly DependencyProperty GlyphWidthProperty = DependencyPropertyManager.Register("GlyphWidth", typeof(double?), typeof(SimpleButton), new FrameworkPropertyMetadata(null));
        public static readonly DependencyProperty ButtonKindProperty = DependencyPropertyManager.Register("ButtonKind", typeof(DevExpress.Xpf.Editors.ButtonKind), typeof(SimpleButton), new FrameworkPropertyMetadata(DevExpress.Xpf.Editors.ButtonKind.Simple, null, (d, e) => ((SimpleButton) d).OnButtonKindChanging(d, e)));
        public static readonly DependencyProperty IsCheckedProperty;
        public static readonly DependencyProperty DelayProperty;
        public static readonly DependencyProperty IntervalProperty;
        public static readonly DependencyProperty IsThreeStateProperty;
        public static readonly DependencyProperty GlyphAlignmentProperty;
        public static readonly RoutedEvent CheckedEvent;
        public static readonly RoutedEvent UncheckedEvent;
        public static readonly RoutedEvent IndeterminateEvent;
        private DispatcherTimer timer;

        public event RoutedEventHandler Checked
        {
            add
            {
                base.AddHandler(CheckedEvent, value);
            }
            remove
            {
                base.RemoveHandler(CheckedEvent, value);
            }
        }

        public event RoutedEventHandler Indeterminate
        {
            add
            {
                base.AddHandler(IndeterminateEvent, value);
            }
            remove
            {
                base.RemoveHandler(IndeterminateEvent, value);
            }
        }

        public event RoutedEventHandler Unchecked
        {
            add
            {
                base.AddHandler(UncheckedEvent, value);
            }
            remove
            {
                base.RemoveHandler(UncheckedEvent, value);
            }
        }

        static SimpleButton()
        {
            FrameworkPropertyMetadata typeMetadata = new FrameworkPropertyMetadata(false, (d, e) => ((SimpleButton) d).OnIsCheckedChanged((bool?) e.OldValue, (bool?) e.NewValue));
            typeMetadata.BindsTwoWayByDefault = true;
            IsCheckedProperty = ToggleButton.IsCheckedProperty.AddOwner(typeof(SimpleButton), typeMetadata);
            DelayProperty = RepeatButton.DelayProperty.AddOwner(typeof(SimpleButton), new FrameworkPropertyMetadata(RepeatButton.DelayProperty.GetMetadata(typeof(RepeatButton)).DefaultValue));
            IntervalProperty = RepeatButton.IntervalProperty.AddOwner(typeof(SimpleButton), new FrameworkPropertyMetadata(RepeatButton.IntervalProperty.GetMetadata(typeof(RepeatButton)).DefaultValue));
            IsThreeStateProperty = ToggleButton.IsThreeStateProperty.AddOwner(typeof(SimpleButton), new FrameworkPropertyMetadata(false));
            GlyphAlignmentProperty = DependencyProperty.Register("GlyphAlignment", typeof(Dock), typeof(SimpleButton), new FrameworkPropertyMetadata(Dock.Left));
            CheckedEvent = ToggleButton.CheckedEvent.AddOwner(typeof(SimpleButton));
            UncheckedEvent = ToggleButton.UncheckedEvent.AddOwner(typeof(SimpleButton));
            IndeterminateEvent = ToggleButton.IndeterminateEvent.AddOwner(typeof(SimpleButton));
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(SimpleButton), new FrameworkPropertyMetadata(typeof(SimpleButton)));
        }

        public override void OnApplyTemplate()
        {
            this.Chrome = (ButtonChrome) base.GetTemplateChild("PART_Owner");
        }

        protected virtual object OnButtonKindChanging(DependencyObject d, object baseValue) => 
            (d is DropDownButton) ? DevExpress.Xpf.Editors.ButtonKind.Simple : baseValue;

        protected internal virtual void OnChecked(RoutedEventArgs e)
        {
            base.RaiseEvent(e);
        }

        protected override void OnClick()
        {
            if (this.ButtonKind == DevExpress.Xpf.Editors.ButtonKind.Toggle)
            {
                this.OnToggle();
            }
            base.OnClick();
        }

        private void OnEnterDown(KeyEventArgs e)
        {
            if ((this.timer == null) || !this.timer.IsEnabled)
            {
                base.OnKeyDown(e);
            }
            this.RepeatButtonTimerStart();
            e.Handled = true;
        }

        protected virtual object OnGlyphChanging(object e)
        {
            ImageSource source = e as ImageSource;
            if ((source == null) || source.IsFrozen)
            {
                return e;
            }
            BitmapImage image = source as BitmapImage;
            if (image != null)
            {
                IUriContext context = source as IUriContext;
                if ((context != null) && ((image.UriSource != null) && (!image.UriSource.IsAbsoluteUri && (context.BaseUri == null))))
                {
                    context.BaseUri = BaseUriHelper.GetBaseUri(this);
                }
            }
            return source;
        }

        protected internal virtual void OnIndeterminate(RoutedEventArgs e)
        {
            base.RaiseEvent(e);
        }

        protected virtual void OnIsCheckedChanged(bool? oldValue, bool? newValue)
        {
            bool? nullable = newValue;
            if (nullable != null)
            {
                bool valueOrDefault = nullable.GetValueOrDefault();
                if (!valueOrDefault)
                {
                    this.OnUnchecked(new RoutedEventArgs(UncheckedEvent));
                    return;
                }
                if (valueOrDefault)
                {
                    this.OnChecked(new RoutedEventArgs(CheckedEvent));
                    return;
                }
            }
            this.OnIndeterminate(new RoutedEventArgs(IndeterminateEvent));
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                this.OnEnterDown(e);
            }
            else
            {
                base.OnKeyDown(e);
            }
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            if (e.Key == Key.Return)
            {
                this.RepeatButtonTimerStop();
            }
        }

        protected override void OnLostMouseCapture(MouseEventArgs e)
        {
            this.RepeatButtonTimerStop();
            base.OnLostMouseCapture(e);
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.RepeatButtonTimerStart();
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            this.RepeatButtonTimerStop();
            base.OnMouseLeftButtonUp(e);
        }

        private void OnTimeout()
        {
            TimeSpan span = TimeSpan.FromMilliseconds((double) this.Interval);
            if (this.timer.Interval != span)
            {
                this.timer.Interval = span;
            }
            this.OnClick();
        }

        private void OnTimeout(object sender, EventArgs e)
        {
            this.OnTimeout();
        }

        private void OnToggle()
        {
            bool? nullable = null;
            bool? isChecked = this.IsChecked;
            bool flag = true;
            if (!((isChecked.GetValueOrDefault() == flag) ? (isChecked != null) : false))
            {
                nullable = new bool?(this.IsChecked != null);
            }
            else
            {
                bool? nullable1;
                if (!this.IsThreeState)
                {
                    nullable1 = false;
                }
                else
                {
                    isChecked = null;
                    nullable1 = isChecked;
                }
                nullable = nullable1;
            }
            base.SetCurrentValue(IsCheckedProperty, nullable);
        }

        protected internal virtual void OnUnchecked(RoutedEventArgs e)
        {
            base.RaiseEvent(e);
        }

        private void RepeatButtonTimerStart()
        {
            if (this.ButtonKind == DevExpress.Xpf.Editors.ButtonKind.Repeat)
            {
                this.StartTimer();
            }
        }

        private void RepeatButtonTimerStop()
        {
            if (this.ButtonKind == DevExpress.Xpf.Editors.ButtonKind.Repeat)
            {
                this.StopTimer();
            }
        }

        protected void StartTimer()
        {
            if (this.timer == null)
            {
                this.timer = new DispatcherTimer();
                this.timer.Tick += new EventHandler(this.OnTimeout);
            }
            else if (this.timer.IsEnabled)
            {
                return;
            }
            this.timer.Interval = TimeSpan.FromMilliseconds((double) this.Delay);
            this.timer.Start();
        }

        protected void StopTimer()
        {
            if (this.timer == null)
            {
                DispatcherTimer timer = this.timer;
            }
            else
            {
                this.timer.Stop();
            }
        }

        protected internal ButtonChrome Chrome { get; set; }

        public ImageSource Glyph
        {
            get => 
                (ImageSource) base.GetValue(GlyphProperty);
            set => 
                base.SetValue(GlyphProperty, value);
        }

        public Dock GlyphAlignment
        {
            get => 
                (Dock) base.GetValue(GlyphAlignmentProperty);
            set => 
                base.SetValue(GlyphAlignmentProperty, value);
        }

        public double GlyphToContentOffset
        {
            get => 
                (double) base.GetValue(GlyphToContentOffsetProperty);
            set => 
                base.SetValue(GlyphToContentOffsetProperty, value);
        }

        public double? GlyphHeight
        {
            get => 
                (double?) base.GetValue(GlyphHeightProperty);
            set => 
                base.SetValue(GlyphHeightProperty, value);
        }

        public double? GlyphWidth
        {
            get => 
                (double?) base.GetValue(GlyphWidthProperty);
            set => 
                base.SetValue(GlyphWidthProperty, value);
        }

        public DevExpress.Xpf.Editors.ButtonKind ButtonKind
        {
            get => 
                (DevExpress.Xpf.Editors.ButtonKind) base.GetValue(ButtonKindProperty);
            set => 
                base.SetValue(ButtonKindProperty, value);
        }

        public bool? IsChecked
        {
            get => 
                (bool?) base.GetValue(IsCheckedProperty);
            set => 
                base.SetValue(IsCheckedProperty, value);
        }

        public int Delay
        {
            get => 
                (int) base.GetValue(DelayProperty);
            set => 
                base.SetValue(DelayProperty, value);
        }

        public int Interval
        {
            get => 
                (int) base.GetValue(IntervalProperty);
            set => 
                base.SetValue(IntervalProperty, value);
        }

        public bool IsThreeState
        {
            get => 
                (bool) base.GetValue(IsThreeStateProperty);
            set => 
                base.SetValue(IsThreeStateProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SimpleButton.<>c <>9 = new SimpleButton.<>c();

            internal object <.cctor>b__13_0(DependencyObject d, object e) => 
                ((SimpleButton) d).OnGlyphChanging(e);

            internal object <.cctor>b__13_1(DependencyObject d, object e) => 
                ((SimpleButton) d).OnButtonKindChanging(d, e);

            internal void <.cctor>b__13_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((SimpleButton) d).OnIsCheckedChanged((bool?) e.OldValue, (bool?) e.NewValue);
            }
        }
    }
}

