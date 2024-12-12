namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Threading;

    public class RenderButtonContext : RenderContentControlContext, ICommandSource
    {
        private static readonly int DefaultKeyboardDelay;
        private static readonly int DefaultKeyboardSpeed;
        private bool? isChecked;
        private int delay;
        private int interval;
        private System.Windows.Controls.ClickMode? clickMode;
        private DevExpress.Xpf.Editors.ButtonKind? buttonKind;
        private bool canRaiseClick;
        private ICommand command;
        private object commandParameter;
        private IInputElement commandTarget;
        private readonly CanExecuteChangedEventHandler<RenderButtonContext> handler;
        private DispatcherTimer timer;

        public event EventHandler CheckChanged;

        public event RenderEventHandler Click;

        static RenderButtonContext();
        public RenderButtonContext(RenderButton factory);
        private void ButtonKindChanged(DevExpress.Xpf.Editors.ButtonKind? value);
        private void CanExecute(object o, EventArgs arg3);
        private void CheckedChanged();
        private void CommandChanged(ICommand oldCommand, ICommand newCommand);
        public void DoClick();
        private static int GetKeyboardDelay();
        internal static int GetKeyboardSpeed();
        protected virtual void OnClick(RenderEventArgs args);
        protected override bool OnIsEnabledChanging(bool value);
        protected override void OnLostMouseCapture();
        protected override void OnMouseDown(MouseRenderEventArgs e);
        protected override void OnMouseEnter(MouseRenderEventArgs args);
        protected override void OnMouseLeave(MouseRenderEventArgs args);
        protected override void OnMouseMove(MouseRenderEventArgs args);
        protected override void OnMouseUp(MouseRenderEventArgs args);
        private void OnTimeout(object sender, EventArgs e);
        private void OnToggle();
        private void RaiseCheckChangedEvent(EventArgs args);
        private void RaiseClickEvent(RenderEventArgs args);
        public override void Release();
        private void StartTimer();
        private void StopTimer();
        protected virtual void UpdateCheckedState();
        public override void UpdateStates();

        public int Delay { get; set; }

        public int Interval { get; set; }

        public ICommand Command { get; set; }

        public object CommandParameter { get; set; }

        public IInputElement CommandTarget { get; set; }

        public DevExpress.Xpf.Editors.ButtonKind? ButtonKind { get; set; }

        public System.Windows.Controls.ClickMode? ClickMode { get; set; }

        public bool? IsChecked { get; set; }

        protected System.Windows.Controls.ClickMode ActualClickMode { get; }

        protected DevExpress.Xpf.Editors.ButtonKind ActualButtonKind { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly RenderButtonContext.<>c <>9;
            public static Action<RenderButtonContext, object, EventArgs> <>9__48_0;

            static <>c();
            internal void <.ctor>b__48_0(RenderButtonContext owner, object o, EventArgs e);
        }
    }
}

