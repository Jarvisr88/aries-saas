namespace DevExpress.Xpf.Bars
{
    using DevExpress.Xpf.Bars.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;

    [Browsable(false)]
    public class BarQuickCustomizationButton : ToggleButton, IEventListenerClient, IMultipleElementRegistratorSupport, IBarNameScopeSupport, IInputElement, INavigationElement, IBarsNavigationSupport
    {
        private bool isSelected;
        public static readonly DependencyProperty OrientationProperty;

        static BarQuickCustomizationButton();
        public BarQuickCustomizationButton();
        object IMultipleElementRegistratorSupport.GetName(object registratorKey);
        bool INavigationElement.ProcessKeyDown(KeyEventArgs e);
        bool IEventListenerClient.ReceiveEvent(object sender, EventArgs e);
        public override void OnApplyTemplate();
        protected override void OnChecked(RoutedEventArgs e);
        private void OnChecked(object sender, RoutedEventArgs e);
        protected override void OnClick();
        protected virtual void OnIsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e);
        protected override void OnIsPressedChanged(DependencyPropertyChangedEventArgs e);
        protected virtual void OnIsSelectedChanged(bool oldValue);
        protected override void OnMouseEnter(MouseEventArgs e);
        protected override void OnMouseLeave(MouseEventArgs e);
        protected override void OnMouseMove(MouseEventArgs e);
        protected virtual void OnOrientationPropertyChanged();
        protected override void OnUnchecked(RoutedEventArgs e);
        private void OnUnchecked(object sender, RoutedEventArgs e);
        protected virtual void UpdateState();
        protected virtual void UpdateStateBeOrientation();

        public System.Windows.Controls.Orientation Orientation { get; set; }

        protected DevExpress.Xpf.Bars.BarControl BarControl { get; }

        IEnumerable<object> IMultipleElementRegistratorSupport.RegistratorKeys { [IteratorStateMachine(typeof(BarQuickCustomizationButton.<DevExpress-Xpf-Bars-IMultipleElementRegistratorSupport-get_RegistratorKeys>d__26))] get; }

        INavigationOwner INavigationElement.BoundOwner { get; }

        bool IBarsNavigationSupport.IsSelected { get; set; }

        bool IBarsNavigationSupport.ExcludeFromNavigation { get; }

        IBarsNavigationSupport IBarsNavigationSupport.Parent { get; }

        int IBarsNavigationSupport.ID { get; }

        bool IBarsNavigationSupport.IsSelectable { get; }

        bool IBarsNavigationSupport.ExitNavigationOnMouseUp { get; }

        bool IBarsNavigationSupport.ExitNavigationOnFocusChangedWithin { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BarQuickCustomizationButton.<>c <>9;
            public static Func<FrameworkElement, DependencyObject> <>9__7_0;
            public static Func<BarManagerCustomizationHelper, PopupMenu> <>9__29_0;
            public static Func<PopupMenu, PopupMenuBarControl> <>9__29_1;
            public static Func<BarControl, bool> <>9__36_0;

            static <>c();
            internal void <.cctor>b__45_0(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal bool <DevExpress.Xpf.Bars.IBarsNavigationSupport.get_Parent>b__36_0(BarControl x);
            internal PopupMenu <DevExpress.Xpf.Bars.INavigationElement.get_BoundOwner>b__29_0(BarManagerCustomizationHelper x);
            internal PopupMenuBarControl <DevExpress.Xpf.Bars.INavigationElement.get_BoundOwner>b__29_1(PopupMenu x);
            internal DependencyObject <get_BarControl>b__7_0(FrameworkElement x);
        }

        [CompilerGenerated]
        private sealed class <DevExpress-Xpf-Bars-IMultipleElementRegistratorSupport-get_RegistratorKeys>d__26 : IEnumerable<object>, IEnumerable, IEnumerator<object>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private object <>2__current;
            private int <>l__initialThreadId;

            [DebuggerHidden]
            public <DevExpress-Xpf-Bars-IMultipleElementRegistratorSupport-get_RegistratorKeys>d__26(int <>1__state);
            private bool MoveNext();
            [DebuggerHidden]
            IEnumerator<object> IEnumerable<object>.GetEnumerator();
            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator();
            [DebuggerHidden]
            void IEnumerator.Reset();
            [DebuggerHidden]
            void IDisposable.Dispose();

            object IEnumerator<object>.Current { [DebuggerHidden] get; }

            object IEnumerator.Current { [DebuggerHidden] get; }
        }
    }
}

