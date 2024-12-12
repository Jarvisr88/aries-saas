namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Editors.Validation;
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    public class FakeBaseEdit : IBaseEditWrapper
    {
        public static readonly IBaseEditWrapper Instance = new FakeBaseEdit();

        event EditValueChangedEventHandler IBaseEditWrapper.EditValueChanged
        {
            add
            {
            }
            remove
            {
            }
        }

        private FakeBaseEdit()
        {
        }

        void IBaseEditWrapper.ClearEditorError()
        {
        }

        bool IBaseEditWrapper.DoValidate() => 
            true;

        void IBaseEditWrapper.FlushPendingEditActions()
        {
        }

        bool IBaseEditWrapper.IsActivatingKey(KeyEventArgs e) => 
            false;

        bool IBaseEditWrapper.IsChildElement(IInputElement element, DependencyObject root) => 
            false;

        void IBaseEditWrapper.LockEditorFocus()
        {
        }

        bool IBaseEditWrapper.NeedsKey(KeyEventArgs e) => 
            true;

        void IBaseEditWrapper.ProcessActivatingKey(KeyEventArgs e)
        {
        }

        void IBaseEditWrapper.ResetEditorCache()
        {
        }

        void IBaseEditWrapper.SelectAll()
        {
        }

        void IBaseEditWrapper.SetDisplayTemplate(ControlTemplate template)
        {
        }

        void IBaseEditWrapper.SetEditTemplate(ControlTemplate template)
        {
        }

        void IBaseEditWrapper.SetKeyboardFocus()
        {
        }

        void IBaseEditWrapper.SetValidationError(BaseValidationError validationError)
        {
        }

        void IBaseEditWrapper.SetValidationErrorTemplate(DataTemplate template)
        {
        }

        void IBaseEditWrapper.UnlockEditorFocus()
        {
        }

        bool IBaseEditWrapper.IsReadOnly
        {
            get => 
                true;
            set
            {
            }
        }

        bool IBaseEditWrapper.ShowEditorButtons
        {
            get => 
                false;
            set
            {
            }
        }

        bool IBaseEditWrapper.IsEditorActive =>
            false;

        bool IBaseEditWrapper.IsValueChanged
        {
            get => 
                false;
            set
            {
            }
        }

        EditMode IBaseEditWrapper.EditMode
        {
            get => 
                EditMode.InplaceInactive;
            set
            {
            }
        }

        object IBaseEditWrapper.EditValue
        {
            get => 
                null;
            set
            {
            }
        }

        HorizontalAlignment IBaseEditWrapper.HorizontalContentAlignment =>
            HorizontalAlignment.Left;

        bool IBaseEditWrapper.CanHandleBubblingEvent =>
            true;
    }
}

