namespace DevExpress.Xpf.Editors.Helpers
{
    using System;
    using System.Windows.Controls;
    using System.Windows.Input;

    public static class TextBoxHelper
    {
        public static bool NeedKey(TextBox textBox, Key key)
        {
            if (textBox != null)
            {
                switch (key)
                {
                    case Key.Prior:
                    case Key.Up:
                        return ((textBox.GetLineIndexFromCharacterIndex(textBox.SelectionStart) != 0) && !textBox.IsReadOnly);

                    case Key.Next:
                    case Key.Down:
                        return ((textBox.GetLineIndexFromCharacterIndex(textBox.SelectionStart) != (textBox.LineCount - 1)) && !textBox.IsReadOnly);

                    case Key.End:
                    case Key.Right:
                        return ((textBox.SelectionStart != textBox.Text.Length) && !textBox.IsReadOnly);

                    case Key.Home:
                    case Key.Left:
                        return (((textBox.SelectionStart != 0) || (textBox.SelectionLength != 0)) ? !textBox.IsReadOnly : false);
                }
            }
            return true;
        }
    }
}

