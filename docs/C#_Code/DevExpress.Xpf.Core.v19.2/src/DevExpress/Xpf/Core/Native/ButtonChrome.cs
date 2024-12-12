namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;
    using System.Windows.Media;

    public class ButtonChrome : Chrome
    {
        public static readonly DependencyProperty IsDefaultedProperty;
        private ButtonBase owner;

        static ButtonChrome();
        protected override void ForegroundChanged(Brush newValue);
        protected override FrameworkRenderElementContext InitializeContext();
        private static bool IsDefaultState(Button ownerButton);
        protected virtual bool IsReadyForUpdate(ButtonBase buttonBase);
        protected virtual void OnApplyRenderTemplate();
        protected virtual void OnIsCheckedChanged(object sender, RoutedEventArgs e);
        protected virtual void OnOwnerChanged();
        protected virtual void OnOwnerIsFocusedChanged(object sender, RoutedEventArgs e);
        private void OnOwnerLoadedChanged(object sender, RoutedEventArgs e);
        protected virtual void OnOwnerMouseEnter(object sender, MouseEventArgs e);
        protected virtual void OnOwnerMouseLeave(object sender, MouseEventArgs e);
        protected virtual void OnOwnerMouseLeftButtonDown(object sender, MouseButtonEventArgs e);
        protected virtual void OnOwnerMouseLeftButtonUp(object sender, MouseButtonEventArgs e);
        protected virtual void OnOwnerMouseMove(object sender, MouseEventArgs e);
        protected override void ReleaseContext(FrameworkRenderElementContext context);
        protected virtual void SetDefaultState();
        protected virtual void SetFocusState();
        protected virtual void SubscribeEvents(ButtonBase ownerButtonBase);
        protected virtual void UnsubscribeEvents(ButtonBase ownerButtonBase);
        protected internal virtual void UpdateStates();

        public RenderButtonBorderContext ContentPart { get; private set; }

        public ButtonBase Owner { get; set; }

        public bool IsDefaulted { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ButtonChrome.<>c <>9;

            static <>c();
            internal void <.cctor>b__1_0(DependencyObject d, DependencyPropertyChangedEventArgs e);
        }
    }
}

