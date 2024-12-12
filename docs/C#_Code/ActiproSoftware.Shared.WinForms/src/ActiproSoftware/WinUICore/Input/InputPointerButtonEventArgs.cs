namespace ActiproSoftware.WinUICore.Input
{
    using ActiproSoftware.WinUICore;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class InputPointerButtonEventArgs : InputPointerEventArgs
    {
        public InputPointerButtonEventArgs(IInputElement originalSource, MouseEventArgs e, InputPointerButtonKind buttonKind) : base(originalSource, e)
        {
            this.ButtonKind = buttonKind;
        }

        public InputPointerButtonKind ButtonKind { get; private set; }

        public bool IsPrimaryButton =>
            this.ButtonKind == InputPointerButtonKind.Primary;

        public MouseEventArgs WrappedEventArgs =>
            this.WrappedEventArgs;
    }
}

