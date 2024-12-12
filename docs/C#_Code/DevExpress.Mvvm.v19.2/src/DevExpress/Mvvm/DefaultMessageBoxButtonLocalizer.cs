namespace DevExpress.Mvvm
{
    using System;
    using System.ComponentModel;
    using System.Windows;

    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public class DefaultMessageBoxButtonLocalizer : IMessageBoxButtonLocalizer
    {
        private DefaultMessageButtonLocalizer localizer = new DefaultMessageButtonLocalizer();

        public string Localize(MessageBoxResult button) => 
            this.localizer.Localize(button.ToMessageResult());
    }
}

