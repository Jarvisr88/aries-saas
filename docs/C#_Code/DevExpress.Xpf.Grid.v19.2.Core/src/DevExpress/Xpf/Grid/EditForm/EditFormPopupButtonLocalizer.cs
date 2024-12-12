namespace DevExpress.Xpf.Grid.EditForm
{
    using DevExpress.Mvvm;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Windows;

    internal class EditFormPopupButtonLocalizer : IMessageBoxButtonLocalizer
    {
        private static string Localize(GridControlStringId id) => 
            GridControlLocalizer.GetString(id);

        public string Localize(MessageBoxResult button) => 
            (button == MessageBoxResult.OK) ? Localize(GridControlStringId.EditForm_UpdateButton) : ((button == MessageBoxResult.Cancel) ? Localize(GridControlStringId.EditForm_CancelButton) : string.Empty);
    }
}

