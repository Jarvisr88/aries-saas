namespace DevExpress.Xpf.Core
{
    using DevExpress.Mvvm;
    using System;
    using System.ComponentModel;
    using System.Windows;

    [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
    public class DXDialogWindowMessageBoxButtonLocalizer : IMessageBoxButtonLocalizer
    {
        public string Localize(MessageBoxResult button)
        {
            switch (button)
            {
                case MessageBoxResult.OK:
                    return DXMessageBoxLocalizer.GetString(DXMessageBoxStringId.Ok);

                case MessageBoxResult.Cancel:
                    return DXMessageBoxLocalizer.GetString(DXMessageBoxStringId.Cancel);

                case MessageBoxResult.Yes:
                    return DXMessageBoxLocalizer.GetString(DXMessageBoxStringId.Yes);

                case MessageBoxResult.No:
                    return DXMessageBoxLocalizer.GetString(DXMessageBoxStringId.No);
            }
            return string.Empty;
        }
    }
}

