namespace DevExpress.Xpf.Editors
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Utils;
    using DevExpress.Xpf.Utils.About;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Threading;

    [LicenseProvider(typeof(DX_WPFEditors_LicenseProvider)), DXToolboxBrowsable(DXToolboxItemKind.Free)]
    public class Calculator : Control, ICalculatorViewOwner
    {
        internal const int MaxPrecision = 0x1c;
        public static readonly DependencyProperty DisplayTextProperty;
        public static readonly DependencyProperty HasErrorProperty;
        public static readonly DependencyProperty HistoryProperty;
        public static readonly DependencyProperty IsDigitalDisplayProperty;
        public static readonly DependencyProperty MemoryProperty;
        public static readonly DependencyProperty PrecisionProperty;
        public static readonly DependencyProperty ShowBorderProperty;
        public static readonly DependencyProperty ShowFocusedStateProperty;
        public static readonly DependencyProperty ValueProperty;
        protected static readonly DependencyPropertyKey DisplayTextPropertyKey;
        protected static readonly DependencyPropertyKey HasErrorPropertyKey;
        protected static readonly DependencyPropertyKey HistoryPropertyKey;
        protected static readonly DependencyPropertyKey MemoryPropertyKey;
        public static readonly RoutedEvent CustomErrorTextEvent;
        public static readonly RoutedEvent ValueChangedEvent;
        private ButtonClickAnimationData buttonClickAnimationData;

        public event CalculatorCustomErrorTextEventHandler CustomErrorText
        {
            add
            {
                base.AddHandler(CustomErrorTextEvent, value);
            }
            remove
            {
                base.RemoveHandler(CustomErrorTextEvent, value);
            }
        }

        public event CalculatorValueChangedEventHandler ValueChanged
        {
            add
            {
                base.AddHandler(ValueChangedEvent, value);
            }
            remove
            {
                base.RemoveHandler(ValueChangedEvent, value);
            }
        }

        static Calculator()
        {
            Type ownerType = typeof(Calculator);
            DisplayTextPropertyKey = DependencyPropertyManager.RegisterReadOnly("DisplayText", typeof(string), ownerType, new PropertyMetadata((d, e) => ((Calculator) d).PropertyChangedDisplayText((string) e.OldValue)));
            DisplayTextProperty = DisplayTextPropertyKey.DependencyProperty;
            HasErrorPropertyKey = DependencyPropertyManager.RegisterReadOnly("HasError", typeof(bool), typeof(Calculator), new PropertyMetadata((d, e) => ((Calculator) d).PropertyChangedHasError()));
            HasErrorProperty = HasErrorPropertyKey.DependencyProperty;
            HistoryPropertyKey = DependencyPropertyManager.RegisterReadOnly("History", typeof(ReadOnlyObservableCollection<string>), typeof(Calculator), new PropertyMetadata());
            HistoryProperty = HistoryPropertyKey.DependencyProperty;
            IsDigitalDisplayProperty = DependencyPropertyManager.Register("IsDigitalDisplay", typeof(bool), typeof(Calculator), new PropertyMetadata(true, (d, e) => ((Calculator) d).PropertyChangedIsDigitalDisplay()));
            MemoryPropertyKey = DependencyPropertyManager.RegisterReadOnly("Memory", typeof(decimal), ownerType, new PropertyMetadata((d, e) => ((Calculator) d).PropertyChangedMemory((decimal) e.OldValue)));
            MemoryProperty = MemoryPropertyKey.DependencyProperty;
            PrecisionProperty = DependencyPropertyManager.Register("Precision", typeof(int), ownerType, new PropertyMetadata(6, (d, e) => ((Calculator) d).PropertyChangedPrecision((int) e.OldValue)), new ValidateValueCallback(Calculator.PropertyValueValidatePrecision));
            ShowBorderProperty = DependencyPropertyManager.Register("ShowBorder", typeof(bool), typeof(Calculator), new PropertyMetadata(true, (d, e) => ((Calculator) d).PropertyChangedShowBorder()));
            ShowFocusedStateProperty = DependencyPropertyManager.Register("ShowFocusedState", typeof(bool), typeof(Calculator), new PropertyMetadata(true, (d, e) => ((Calculator) d).PropertyChangedShowFocusedState()));
            ValueProperty = DependencyPropertyManager.Register("Value", typeof(decimal), ownerType, new PropertyMetadata((d, e) => ((Calculator) d).PropertyChangedValue((decimal) e.OldValue)));
            CustomErrorTextEvent = EventManager.RegisterRoutedEvent("CustomErrorText", RoutingStrategy.Direct, typeof(CalculatorCustomErrorTextEventHandler), ownerType);
            ValueChangedEvent = EventManager.RegisterRoutedEvent("ValueChanged", RoutingStrategy.Direct, typeof(CalculatorValueChangedEventHandler), ownerType);
            CommandManager.RegisterClassCommandBinding(ownerType, new CommandBinding(ApplicationCommands.Copy, (d, e) => ((Calculator) d).Copy(), (d, e) => ((Calculator) d).CanCopy(e)));
            CommandManager.RegisterClassCommandBinding(ownerType, new CommandBinding(ApplicationCommands.Paste, (d, e) => ((Calculator) d).Paste(), (d, e) => ((Calculator) d).CanPaste(e)));
        }

        public Calculator()
        {
            ButtonClickAnimationData data = new ButtonClickAnimationData {
                Buttons = new List<ButtonBase>(),
                Timers = new List<DispatcherTimer>()
            };
            this.buttonClickAnimationData = data;
            this.SetDefaultStyleKey(typeof(Calculator));
            this.ButtonClickCommand = DelegateCommandFactory.Create<object>(buttonID => this.View.ProcessButtonClick(buttonID), false);
            this.HistoryList = new ObservableCollection<string>();
            this.History = new ReadOnlyObservableCollection<string>(this.HistoryList);
            this.View = this.CreateView();
            this.View.Precision = this.Precision;
            base.AddHandler(UIElement.MouseDownEvent, new MouseButtonEventHandler(this.OnMouseDownInternal), true);
            this.Reset();
        }

        protected virtual bool CanCopy() => 
            this.View.CanCopy();

        private void CanCopy(CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.CanCopy();
        }

        protected virtual bool CanPaste() => 
            this.View.CanPaste();

        private void CanPaste(CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.CanPaste();
        }

        public virtual void ClearHistory()
        {
            this.HistoryList.Clear();
        }

        protected virtual void Copy()
        {
            this.View.Copy();
        }

        protected virtual CalculatorViewBase CreateView() => 
            new CalculatorStandardView(this);

        void ICalculatorViewOwner.AddToHistory(string text)
        {
            this.HistoryList.Add(text);
        }

        void ICalculatorViewOwner.AnimateButtonClick(object buttonID)
        {
            ButtonBase button = this.GetButton(buttonID);
            if (button != null)
            {
                DispatcherTimer timer;
                VisualStateManager.GoToState(button, "Pressed", true);
                if (this.buttonClickAnimationData.Buttons.Contains(button))
                {
                    timer = this.buttonClickAnimationData.Timers[this.buttonClickAnimationData.Buttons.IndexOf(button)];
                    timer.Stop();
                }
                else
                {
                    this.buttonClickAnimationData.Buttons.Add(button);
                    timer = new DispatcherTimer();
                    this.buttonClickAnimationData.Timers.Add(timer);
                    timer.Interval = TimeSpan.FromMilliseconds(100.0);
                    timer.Tick += new EventHandler(this.OnButtonClickAnimationTimerTick);
                }
                timer.Start();
            }
        }

        void ICalculatorViewOwner.GetCustomErrorText(ref string errorText)
        {
            CalculatorCustomErrorTextEventArgs e = new CalculatorCustomErrorTextEventArgs(errorText);
            base.RaiseEvent(e);
            errorText = e.ErrorText;
        }

        void ICalculatorViewOwner.SetDisplayText(string value)
        {
            this.DisplayText = value;
        }

        void ICalculatorViewOwner.SetHasError(bool value)
        {
            this.HasError = value;
        }

        void ICalculatorViewOwner.SetMemory(decimal value)
        {
            this.Memory = value;
        }

        void ICalculatorViewOwner.SetValue(decimal value)
        {
            this.Value = value;
        }

        protected virtual ButtonBase GetButton(object buttonID) => 
            (ButtonBase) LayoutHelper.FindElement(this, element => this.IsButtonWithID(element, buttonID));

        private bool IsButtonWithID(FrameworkElement element, object buttonID) => 
            (element is ButtonBase) && Equals(buttonID, (element as ButtonBase).CommandParameter);

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (VisualTreeHelper.GetChildrenCount(this) != 0)
            {
                FrameworkElement child = VisualTreeHelper.GetChild(this, 0) as FrameworkElement;
                if (child != null)
                {
                    child.DataContext = this;
                }
            }
            this.UpdateVisualState(false);
        }

        private void OnButtonClickAnimationTimerTick(object sender, EventArgs e)
        {
            DispatcherTimer item = (DispatcherTimer) sender;
            item.Stop();
            item.Tick -= new EventHandler(this.OnButtonClickAnimationTimerTick);
            int index = this.buttonClickAnimationData.Timers.IndexOf(item);
            this.buttonClickAnimationData.Timers.RemoveAt(index);
            ButtonBase control = this.buttonClickAnimationData.Buttons[index];
            this.buttonClickAnimationData.Buttons.RemoveAt(index);
            VisualStateManager.GoToState(control, control.IsMouseOver ? "MouseOver" : "Normal", true);
        }

        protected override void OnGotKeyboardFocus(KeyboardFocusChangedEventArgs e)
        {
            base.OnGotKeyboardFocus(e);
            this.UpdateVisualState(true);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (!e.Handled)
            {
                this.View.OnKeyDown(e);
            }
        }

        protected override void OnLostKeyboardFocus(KeyboardFocusChangedEventArgs e)
        {
            base.OnLostKeyboardFocus(e);
            this.UpdateVisualState(true);
        }

        private void OnMouseDownInternal(object sender, MouseButtonEventArgs e)
        {
            base.Focus();
        }

        protected override void OnTextInput(TextCompositionEventArgs e)
        {
            base.OnTextInput(e);
            if (!e.Handled)
            {
                this.View.OnTextInput(e);
            }
        }

        protected virtual void Paste()
        {
            this.View.Paste();
        }

        protected virtual void PropertyChangedDisplayText(string oldValue)
        {
        }

        protected virtual void PropertyChangedHasError()
        {
            this.UpdateVisualState(true);
        }

        protected virtual void PropertyChangedIsDigitalDisplay()
        {
            this.UpdateVisualState(true);
        }

        protected virtual void PropertyChangedMemory(decimal oldValue)
        {
        }

        protected virtual void PropertyChangedPrecision(int oldValue)
        {
            this.View.Precision = this.Precision;
        }

        protected virtual void PropertyChangedShowBorder()
        {
            this.UpdateVisualState(true);
        }

        protected virtual void PropertyChangedShowFocusedState()
        {
            this.UpdateVisualState(true);
        }

        protected virtual void PropertyChangedValue(decimal oldValue)
        {
            this.View.Value = this.Value;
            this.RaiseValueChanged(oldValue);
        }

        private static bool PropertyValueValidatePrecision(object value)
        {
            int num = (int) value;
            return ((num >= 0) && (num <= 0x1c));
        }

        protected virtual void RaiseValueChanged(decimal oldValue)
        {
            CalculatorValueChangedEventArgs e = new CalculatorValueChangedEventArgs(oldValue, this.Value);
            base.RaiseEvent(e);
        }

        public virtual void Reset()
        {
            this.View.Init(0M, true);
        }

        protected virtual void UpdateVisualState(bool useTransitions)
        {
            VisualStateManager.GoToState(this, this.HasError ? "Error" : "NoError", useTransitions);
            VisualStateManager.GoToState(this, this.IsDigitalDisplay ? "DigitalDisplay" : "TextDisplay", useTransitions);
            VisualStateManager.GoToState(this, this.ShowBorder ? "ShowBorder" : "EmptyBorder", useTransitions);
            VisualStateManager.GoToState(this, (!FocusHelper.IsKeyboardFocusWithin(this) || !this.ShowFocusedState) ? "Unfocused" : "Focused", useTransitions);
        }

        [Description("This member supports the internal infrastructure and cannot be used directly from your code.")]
        public ICommand ButtonClickCommand { get; private set; }

        [Description("Gets the text displayed within the calculator. This is a dependency property.")]
        public string DisplayText
        {
            get => 
                (string) base.GetValue(DisplayTextProperty);
            protected set => 
                base.SetValue(DisplayTextPropertyKey, value);
        }

        [Description("Gets whether the calculator displays an error. This is a dependency property.")]
        public bool HasError
        {
            get => 
                (bool) base.GetValue(HasErrorProperty);
            protected set => 
                base.SetValue(HasErrorPropertyKey, value);
        }

        [Description("Gets the calculation history. This is a dependency property.")]
        public ReadOnlyObservableCollection<string> History
        {
            get => 
                (ReadOnlyObservableCollection<string>) base.GetValue(HistoryProperty);
            protected set => 
                base.SetValue(HistoryPropertyKey, value);
        }

        [Description("Gets or sets whether the calculator has the digital display. This is a dependency property.")]
        public bool IsDigitalDisplay
        {
            get => 
                (bool) base.GetValue(IsDigitalDisplayProperty);
            set => 
                base.SetValue(IsDigitalDisplayProperty, value);
        }

        [Description("Gets a value stored in the calculator's memory. This is a dependency property.")]
        public decimal Memory
        {
            get => 
                (decimal) base.GetValue(MemoryProperty);
            protected set => 
                base.SetValue(MemoryPropertyKey, value);
        }

        [Description("Gets or sets the maximum number of digits displayed to the right of the decimal point. This is a dependency property.")]
        public int Precision
        {
            get => 
                (int) base.GetValue(PrecisionProperty);
            set => 
                base.SetValue(PrecisionProperty, value);
        }

        [Description("Gets or sets whether to show the border. This is a dependency property.")]
        public bool ShowBorder
        {
            get => 
                (bool) base.GetValue(ShowBorderProperty);
            set => 
                base.SetValue(ShowBorderProperty, value);
        }

        [Description("Gets or sets whether the calculator's display is highlighted when the calculator is focused. This is a dependency property.")]
        public bool ShowFocusedState
        {
            get => 
                (bool) base.GetValue(ShowFocusedStateProperty);
            set => 
                base.SetValue(ShowFocusedStateProperty, value);
        }

        [Description("Gets or sets the calculator's value. This is a dependency property.")]
        public decimal Value
        {
            get => 
                (decimal) base.GetValue(ValueProperty);
            set => 
                base.SetValue(ValueProperty, value);
        }

        protected ObservableCollection<string> HistoryList { get; private set; }

        protected internal CalculatorViewBase View { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly Calculator.<>c <>9 = new Calculator.<>c();

            internal void <.cctor>b__16_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((Calculator) d).PropertyChangedDisplayText((string) e.OldValue);
            }

            internal void <.cctor>b__16_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((Calculator) d).PropertyChangedHasError();
            }

            internal void <.cctor>b__16_10(object d, ExecutedRoutedEventArgs e)
            {
                ((Calculator) d).Paste();
            }

            internal void <.cctor>b__16_11(object d, CanExecuteRoutedEventArgs e)
            {
                ((Calculator) d).CanPaste(e);
            }

            internal void <.cctor>b__16_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((Calculator) d).PropertyChangedIsDigitalDisplay();
            }

            internal void <.cctor>b__16_3(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((Calculator) d).PropertyChangedMemory((decimal) e.OldValue);
            }

            internal void <.cctor>b__16_4(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((Calculator) d).PropertyChangedPrecision((int) e.OldValue);
            }

            internal void <.cctor>b__16_5(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((Calculator) d).PropertyChangedShowBorder();
            }

            internal void <.cctor>b__16_6(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((Calculator) d).PropertyChangedShowFocusedState();
            }

            internal void <.cctor>b__16_7(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((Calculator) d).PropertyChangedValue((decimal) e.OldValue);
            }

            internal void <.cctor>b__16_8(object d, ExecutedRoutedEventArgs e)
            {
                ((Calculator) d).Copy();
            }

            internal void <.cctor>b__16_9(object d, CanExecuteRoutedEventArgs e)
            {
                ((Calculator) d).CanCopy(e);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct ButtonClickAnimationData
        {
            public List<ButtonBase> Buttons;
            public List<DispatcherTimer> Timers;
        }
    }
}

