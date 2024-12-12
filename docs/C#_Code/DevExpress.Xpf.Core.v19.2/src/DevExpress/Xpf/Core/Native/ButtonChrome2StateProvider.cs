namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;

    public class ButtonChrome2StateProvider : ChromeStateProviderBase
    {
        public static readonly string DisabledStateName;
        public static readonly string NormalStateName;
        public static readonly string MouseOverStateName;
        public static readonly string PressedStateName;
        public static readonly string CheckedStateName;
        public static readonly string UncheckedStateName;
        public static readonly string IndeterminateStateName;
        public static readonly string FocusedStateName;
        private static readonly List<string> StringPriorities;

        static ButtonChrome2StateProvider();
        protected string CombineState(string current, string additional);
        public override string GetActualForegroundStateName(IEnumerable<string> availableStates);
        public override string GetBaseState(string desiredState, IEnumerable<string> allStates);
        protected virtual string GetCommonState();
        protected virtual string GetFocusedState();
        protected override string GetState();
        protected virtual string GetToggleState();
        private static void OnButtonChrome2MouseDown(object sender, MouseButtonEventArgs e);
        protected virtual void OnCapture(object sender, MouseEventArgs e);
        protected virtual void OnLostCapture(object sender, MouseEventArgs e);
        protected virtual void OnSourceChecked(object sender, RoutedEventArgs e);
        protected virtual void OnSourceGotFocus(object sender, RoutedEventArgs e);
        protected virtual void OnSourceGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e);
        protected virtual void OnSourceIsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e);
        protected virtual void OnSourceKeyDown(object sender, KeyEventArgs e);
        protected virtual void OnSourceKeyUp(object sender, KeyEventArgs e);
        protected virtual void OnSourceLostFocus(object sender, RoutedEventArgs e);
        protected virtual void OnSourceLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e);
        protected virtual void OnSourceMouseDown(object sender, MouseButtonEventArgs e);
        protected virtual void OnSourceMouseEnter(object sender, MouseEventArgs e);
        protected virtual void OnSourceMouseLeave(object sender, MouseEventArgs e);
        protected virtual void OnSourceMouseMove(object sender, MouseEventArgs e);
        protected virtual void OnSourceMouseUp(object sender, MouseButtonEventArgs e);
        protected virtual void OnSourceUnchecked(object sender, RoutedEventArgs e);
        protected override void Subscribe(FrameworkElement source);
        protected override void Unsubscribe(FrameworkElement source);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ButtonChrome2StateProvider.<>c <>9;
            public static Func<FrameworkElement, bool> <>9__16_0;

            static <>c();
            internal bool <GetFocusedState>b__16_0(FrameworkElement x);
        }
    }
}

