namespace ActiproSoftware.WinUICore.Input
{
    using #H;
    using ActiproSoftware.WinUICore;
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Windows.Forms;

    public class InputPointerWheelEventArgs : InputPointerEventArgs
    {
        public InputPointerWheelEventArgs(IInputElement originalSource, MouseEventArgs e) : base(originalSource, e)
        {
        }

        public override string ToString()
        {
            object[] args = new object[] { this.Delta };
            return (this.ToString() + string.Format(CultureInfo.InvariantCulture, #G.#eg(0xe90), args));
        }

        public int Delta =>
            this.WrappedEventArgs.Delta;

        public bool IsHorizontal =>
            false;

        public int ScrollLines =>
            SystemInformation.MouseWheelScrollLines;

        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public int SingleUnitDelta =>
            SystemInformation.MouseWheelScrollDelta;

        public MouseEventArgs WrappedEventArgs =>
            this.WrappedEventArgs;
    }
}

