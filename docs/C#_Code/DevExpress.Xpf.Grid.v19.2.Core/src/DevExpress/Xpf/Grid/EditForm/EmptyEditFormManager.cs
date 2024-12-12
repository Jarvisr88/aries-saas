namespace DevExpress.Xpf.Grid.EditForm
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Windows;
    using System.Windows.Input;

    public class EmptyEditFormManager : IEditFormManager
    {
        private static EmptyEditFormManager instance;

        public EditFormRowData GetInplaceData(int rowHandle) => 
            null;

        public bool IsInlineFormChild(DependencyObject source) => 
            false;

        public void OnAfterScroll()
        {
        }

        public void OnBeforeScroll(int targetIndex)
        {
        }

        public void OnDoubleClick(MouseButtonEventArgs e)
        {
        }

        public void OnInlineFormClosed(bool success)
        {
        }

        public void OnIsModifiedChanged(bool isModified)
        {
        }

        public void OnPreviewKeyDown(KeyEventArgs e)
        {
        }

        public bool RequestUIUpdate() => 
            true;

        public static EmptyEditFormManager Instance
        {
            get
            {
                instance ??= new EmptyEditFormManager();
                return instance;
            }
        }

        public bool IsEditFormVisible =>
            false;

        public bool IsEditFormModified =>
            false;

        public bool AllowEditForm =>
            false;

        public Locker EditFormOpenLocker =>
            new Locker();
    }
}

