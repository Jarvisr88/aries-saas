namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Windows.Controls;
    using System.Windows.Input;

    public class ListBoxRadioButton : RadioButton
    {
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            e.Handled = false;
        }
    }
}

