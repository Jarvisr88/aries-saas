namespace DevExpress.Xpf.Printing.PreviewControl.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;
    using System.Windows.Input;

    public static class WinFormsCompatibilityExtensions
    {
        public static Keys ToWinFormsModifierKeys(this ModifierKeys key)
        {
            switch (key)
            {
                case ModifierKeys.Alt:
                    return Keys.Alt;

                case ModifierKeys.Control:
                    return Keys.Control;

                case ModifierKeys.Shift:
                    return Keys.Shift;
            }
            return Keys.None;
        }

        public static MouseButtons ToWinFormsMouseButtons(this MouseButton button)
        {
            MouseButtons left;
            switch (button)
            {
                case MouseButton.Left:
                    left = MouseButtons.Left;
                    break;

                case MouseButton.Middle:
                    left = MouseButtons.Middle;
                    break;

                case MouseButton.Right:
                    left = MouseButtons.Right;
                    break;

                case MouseButton.XButton1:
                    left = MouseButtons.XButton1;
                    break;

                case MouseButton.XButton2:
                    left = MouseButtons.XButton2;
                    break;

                default:
                    left = MouseButtons.None;
                    break;
            }
            return left;
        }
    }
}

