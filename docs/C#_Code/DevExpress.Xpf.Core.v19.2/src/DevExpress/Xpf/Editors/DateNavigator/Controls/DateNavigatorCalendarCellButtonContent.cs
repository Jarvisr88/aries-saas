namespace DevExpress.Xpf.Editors.DateNavigator.Controls
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Media;

    public class DateNavigatorCalendarCellButtonContent : Control
    {
        public static readonly DependencyProperty DefaultForegroundSolidColorProperty;
        public static readonly DependencyProperty FocusedForegroundSolidColorProperty;
        public static readonly DependencyProperty HolidayForegroundSolidColorProperty;
        public static readonly DependencyProperty InactiveForegroundSolidColorProperty;
        public static readonly DependencyProperty MouseOverForegroundSolidColorProperty;
        public static readonly DependencyProperty SelectedForegroundSolidColorProperty;
        public static readonly DependencyProperty DisabledForegroundSolidColorProperty;
        public static readonly DependencyProperty TextProperty;
        public static readonly DependencyProperty TodayForegroundSolidColorProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty IsMouseOverInternalProperty;

        static DateNavigatorCalendarCellButtonContent()
        {
            Type ownerType = typeof(DateNavigatorCalendarCellButtonContent);
            DefaultForegroundSolidColorProperty = DependencyPropertyManager.Register("DefaultForegroundSolidColor", typeof(Color), ownerType, new PropertyMetadata((d, e) => ((DateNavigatorCalendarCellButtonContent) d).PropertyChangedForegroundSolidColor()));
            FocusedForegroundSolidColorProperty = DependencyPropertyManager.Register("FocusedForegroundSolidColor", typeof(Color), ownerType, new PropertyMetadata((d, e) => ((DateNavigatorCalendarCellButtonContent) d).PropertyChangedForegroundSolidColor()));
            HolidayForegroundSolidColorProperty = DependencyPropertyManager.Register("HolidayForegroundSolidColor", typeof(Color), ownerType, new PropertyMetadata((d, e) => ((DateNavigatorCalendarCellButtonContent) d).PropertyChangedForegroundSolidColor()));
            InactiveForegroundSolidColorProperty = DependencyPropertyManager.Register("InactiveForegroundSolidColor", typeof(Color), ownerType, new PropertyMetadata((d, e) => ((DateNavigatorCalendarCellButtonContent) d).PropertyChangedForegroundSolidColor()));
            IsMouseOverInternalProperty = DependencyPropertyManager.Register("IsMouseOverInternal", typeof(bool), ownerType, new PropertyMetadata(false, (d, e) => ((DateNavigatorCalendarCellButtonContent) d).PropertyChangedIsMouseOverInternal()));
            MouseOverForegroundSolidColorProperty = DependencyPropertyManager.Register("MouseOverForegroundSolidColor", typeof(Color), ownerType, new PropertyMetadata((d, e) => ((DateNavigatorCalendarCellButtonContent) d).PropertyChangedForegroundSolidColor()));
            SelectedForegroundSolidColorProperty = DependencyPropertyManager.Register("SelectedForegroundSolidColor", typeof(Color), ownerType, new PropertyMetadata((d, e) => ((DateNavigatorCalendarCellButtonContent) d).PropertyChangedForegroundSolidColor()));
            TextProperty = DependencyPropertyManager.Register("Text", typeof(string), ownerType, new PropertyMetadata(null, (d, e) => ((DateNavigatorCalendarCellButtonContent) d).PropertyChangedText()));
            TodayForegroundSolidColorProperty = DependencyPropertyManager.Register("TodayForegroundSolidColor", typeof(Color), ownerType, new PropertyMetadata((d, e) => ((DateNavigatorCalendarCellButtonContent) d).PropertyChangedForegroundSolidColor()));
            DisabledForegroundSolidColorProperty = DependencyPropertyManager.Register("DisabledForegroundSolidColor", typeof(Color), ownerType, new PropertyMetadata((d, e) => ((DateNavigatorCalendarCellButtonContent) d).PropertyChangedForegroundSolidColor()));
        }

        public DateNavigatorCalendarCellButtonContent()
        {
            base.SnapsToDevicePixels = true;
            Binding binding = new Binding("IsMouseOver");
            binding.Source = this;
            base.SetBinding(IsMouseOverInternalProperty, binding);
        }

        private bool IsForegroundSolidColorPropertyAssigned(DependencyProperty dp)
        {
            Color color = new Color();
            return (((Color) base.GetValue(dp)) != color);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.ElementFocused = base.GetTemplateChild("PART_Focused") as UIElement;
            this.ElementMouseOver = base.GetTemplateChild("PART_MouseOver") as UIElement;
            this.ElementSelected = base.GetTemplateChild("PART_Selected") as UIElement;
            this.ElementText = base.GetTemplateChild("PART_Text") as TextBlock;
            this.ElementToday = base.GetTemplateChild("PART_Today") as UIElement;
            this.UpdateElements();
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (ReferenceEquals(e.Property, DateNavigatorCalendar.CellStateProperty))
            {
                this.UpdateElements();
            }
        }

        protected virtual void PropertyChangedForegroundSolidColor()
        {
            this.UpdateForeground();
        }

        protected virtual void PropertyChangedIsMouseOverInternal()
        {
            this.UpdateElements();
        }

        protected virtual void PropertyChangedText()
        {
            this.UpdateElements();
        }

        private void UpdateElements()
        {
            DateNavigatorCalendarCellState cellState = DateNavigatorCalendar.GetCellState(this);
            if (this.ElementFocused != null)
            {
                this.ElementFocused.SetVisible(cellState.HasFlag(DateNavigatorCalendarCellState.Focused));
            }
            if (this.ElementMouseOver != null)
            {
                this.ElementMouseOver.SetVisible(base.IsMouseOver);
            }
            if (this.ElementSelected != null)
            {
                this.ElementSelected.SetVisible(cellState.HasFlag(DateNavigatorCalendarCellState.Selected));
            }
            if (this.ElementText != null)
            {
                this.ElementText.Text = this.Text;
            }
            if (this.ElementToday != null)
            {
                this.ElementToday.SetVisible(cellState.HasFlag(DateNavigatorCalendarCellState.Today));
            }
            if (cellState.HasFlag(DateNavigatorCalendarCellState.Special))
            {
                base.FontWeight = FontWeights.Bold;
            }
            else
            {
                base.ClearValue(Control.FontWeightProperty);
            }
            this.UpdateForeground();
        }

        private void UpdateForeground()
        {
            DateNavigatorCalendarCellState cellState = DateNavigatorCalendar.GetCellState(this);
            if (cellState.HasFlag(DateNavigatorCalendarCellState.Focused) && this.IsForegroundSolidColorPropertyAssigned(FocusedForegroundSolidColorProperty))
            {
                base.Foreground = new SolidColorBrush(this.FocusedForegroundSolidColor);
            }
            else if (cellState.HasFlag(DateNavigatorCalendarCellState.Selected) && this.IsForegroundSolidColorPropertyAssigned(SelectedForegroundSolidColorProperty))
            {
                base.Foreground = new SolidColorBrush(this.SelectedForegroundSolidColor);
            }
            else if (base.IsMouseOver && this.IsForegroundSolidColorPropertyAssigned(MouseOverForegroundSolidColorProperty))
            {
                base.Foreground = new SolidColorBrush(this.MouseOverForegroundSolidColor);
            }
            else if (cellState.HasFlag(DateNavigatorCalendarCellState.Disabled) && this.IsForegroundSolidColorPropertyAssigned(DisabledForegroundSolidColorProperty))
            {
                base.Foreground = new SolidColorBrush(this.DisabledForegroundSolidColor);
            }
            else if (cellState.HasFlag(DateNavigatorCalendarCellState.Today) && this.IsForegroundSolidColorPropertyAssigned(TodayForegroundSolidColorProperty))
            {
                base.Foreground = new SolidColorBrush(this.TodayForegroundSolidColor);
            }
            else if (cellState.HasFlag(DateNavigatorCalendarCellState.Inactive) && this.IsForegroundSolidColorPropertyAssigned(InactiveForegroundSolidColorProperty))
            {
                base.Foreground = new SolidColorBrush(this.InactiveForegroundSolidColor);
            }
            else if (cellState.HasFlag(DateNavigatorCalendarCellState.Holiday) && this.IsForegroundSolidColorPropertyAssigned(HolidayForegroundSolidColorProperty))
            {
                base.Foreground = new SolidColorBrush(this.HolidayForegroundSolidColor);
            }
            else if (this.IsForegroundSolidColorPropertyAssigned(DefaultForegroundSolidColorProperty))
            {
                base.Foreground = new SolidColorBrush(this.DefaultForegroundSolidColor);
            }
            else
            {
                base.ClearValue(Control.ForegroundProperty);
            }
        }

        public Color DefaultForegroundSolidColor
        {
            get => 
                (Color) base.GetValue(DefaultForegroundSolidColorProperty);
            set => 
                base.SetValue(DefaultForegroundSolidColorProperty, value);
        }

        public Color FocusedForegroundSolidColor
        {
            get => 
                (Color) base.GetValue(FocusedForegroundSolidColorProperty);
            set => 
                base.SetValue(FocusedForegroundSolidColorProperty, value);
        }

        public Color HolidayForegroundSolidColor
        {
            get => 
                (Color) base.GetValue(HolidayForegroundSolidColorProperty);
            set => 
                base.SetValue(HolidayForegroundSolidColorProperty, value);
        }

        public Color InactiveForegroundSolidColor
        {
            get => 
                (Color) base.GetValue(InactiveForegroundSolidColorProperty);
            set => 
                base.SetValue(InactiveForegroundSolidColorProperty, value);
        }

        public Color MouseOverForegroundSolidColor
        {
            get => 
                (Color) base.GetValue(MouseOverForegroundSolidColorProperty);
            set => 
                base.SetValue(MouseOverForegroundSolidColorProperty, value);
        }

        public Color SelectedForegroundSolidColor
        {
            get => 
                (Color) base.GetValue(SelectedForegroundSolidColorProperty);
            set => 
                base.SetValue(SelectedForegroundSolidColorProperty, value);
        }

        public Color DisabledForegroundSolidColor
        {
            get => 
                (Color) base.GetValue(DisabledForegroundSolidColorProperty);
            set => 
                base.SetValue(DisabledForegroundSolidColorProperty, value);
        }

        public string Text
        {
            get => 
                (string) base.GetValue(TextProperty);
            set => 
                base.SetValue(TextProperty, value);
        }

        public Color TodayForegroundSolidColor
        {
            get => 
                (Color) base.GetValue(TodayForegroundSolidColorProperty);
            set => 
                base.SetValue(TodayForegroundSolidColorProperty, value);
        }

        protected UIElement ElementFocused { get; private set; }

        protected UIElement ElementMouseOver { get; private set; }

        protected UIElement ElementSelected { get; private set; }

        protected TextBlock ElementText { get; private set; }

        protected UIElement ElementToday { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DateNavigatorCalendarCellButtonContent.<>c <>9 = new DateNavigatorCalendarCellButtonContent.<>c();

            internal void <.cctor>b__10_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DateNavigatorCalendarCellButtonContent) d).PropertyChangedForegroundSolidColor();
            }

            internal void <.cctor>b__10_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DateNavigatorCalendarCellButtonContent) d).PropertyChangedForegroundSolidColor();
            }

            internal void <.cctor>b__10_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DateNavigatorCalendarCellButtonContent) d).PropertyChangedForegroundSolidColor();
            }

            internal void <.cctor>b__10_3(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DateNavigatorCalendarCellButtonContent) d).PropertyChangedForegroundSolidColor();
            }

            internal void <.cctor>b__10_4(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DateNavigatorCalendarCellButtonContent) d).PropertyChangedIsMouseOverInternal();
            }

            internal void <.cctor>b__10_5(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DateNavigatorCalendarCellButtonContent) d).PropertyChangedForegroundSolidColor();
            }

            internal void <.cctor>b__10_6(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DateNavigatorCalendarCellButtonContent) d).PropertyChangedForegroundSolidColor();
            }

            internal void <.cctor>b__10_7(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DateNavigatorCalendarCellButtonContent) d).PropertyChangedText();
            }

            internal void <.cctor>b__10_8(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DateNavigatorCalendarCellButtonContent) d).PropertyChangedForegroundSolidColor();
            }

            internal void <.cctor>b__10_9(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DateNavigatorCalendarCellButtonContent) d).PropertyChangedForegroundSolidColor();
            }
        }
    }
}

