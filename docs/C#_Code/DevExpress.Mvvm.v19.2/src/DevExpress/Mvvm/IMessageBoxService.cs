namespace DevExpress.Mvvm
{
    using System;
    using System.ComponentModel;

    public interface IMessageBoxService
    {
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        MessageResult Show(string messageBoxText, string caption, MessageButton button, MessageIcon icon, MessageResult defaultResult);
    }
}

