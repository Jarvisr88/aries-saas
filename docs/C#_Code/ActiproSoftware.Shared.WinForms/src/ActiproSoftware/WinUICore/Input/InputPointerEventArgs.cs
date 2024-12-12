namespace ActiproSoftware.WinUICore.Input
{
    using #H;
    using ActiproSoftware.WinUICore;
    using ActiproSoftware.WinUICore.Commands;
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Windows.Forms;

    public class InputPointerEventArgs : EventArgs
    {
        private bool #lXd;

        private void #Boi()
        {
            this.DeviceKind = InputDeviceKind.Mouse;
        }

        public InputPointerEventArgs(IInputElement originalSource, MouseEventArgs e)
        {
            if (originalSource == null)
            {
                throw new ArgumentNullException(#G.#eg(0xe22));
            }
            if (e == null)
            {
                throw new ArgumentNullException(#G.#eg(0xe37));
            }
            this.WrappedEventArgs = e;
            this.#Boi();
        }

        public unsafe Point GetPosition(IUIElement relativeTo)
        {
            Point location = this.WrappedEventArgs.Location;
            if ((relativeTo != null) && (relativeTo.Parent != null))
            {
                Rectangle bounds = relativeTo.Bounds;
                Point* pointPtr1 = &location;
                pointPtr1.X -= bounds.X;
                Point* pointPtr2 = &location;
                pointPtr2.Y -= bounds.Y;
            }
            return location;
        }

        public bool IsPositionOver(IUIElement element)
        {
            if (element == null)
            {
                return false;
            }
            Rectangle rectangle = new Rectangle(0, 0, element.Bounds.Width, element.Bounds.Height);
            return rectangle.Contains(this.GetPosition(element));
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat(#G.#eg(0xe3c), this.DeviceKind);
            UIElement originalSource = this.OriginalSource as UIElement;
            if (originalSource != null)
            {
                Point position = this.GetPosition(originalSource);
                builder.AppendFormat(#G.#eg(0xe45), position.X, position.Y);
            }
            if (this.ModifierKeys != ActiproSoftware.WinUICore.Input.ModifierKeys.None)
            {
                builder.AppendFormat(#G.#eg(0xe5a), this.ModifierKeys);
            }
            builder.AppendFormat(#G.#eg(0xe77), this.OriginalSource?.GetType().Name);
            return builder.ToString();
        }

        public InputDeviceKind DeviceKind { get; private set; }

        public bool Handled
        {
            get
            {
                HandledMouseEventArgs wrappedEventArgs = this.WrappedEventArgs as HandledMouseEventArgs;
                return ((wrappedEventArgs == null) ? this.#lXd : wrappedEventArgs.Handled);
            }
            set
            {
                HandledMouseEventArgs wrappedEventArgs;
                if (0 != 0)
                {
                    HandledMouseEventArgs wrappedEventArgs = this.WrappedEventArgs as HandledMouseEventArgs;
                }
                else
                {
                    wrappedEventArgs = this.WrappedEventArgs as HandledMouseEventArgs;
                }
                if (wrappedEventArgs != null)
                {
                    wrappedEventArgs.Handled = value;
                }
                else
                {
                    this.#lXd = value;
                }
            }
        }

        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public ActiproSoftware.WinUICore.Input.ModifierKeys ModifierKeys =>
            CommandLinkCollection.GetCurrentModifierKeys();

        public IInputElement OriginalSource { get; private set; }

        public MouseEventArgs WrappedEventArgs { get; private set; }
    }
}

