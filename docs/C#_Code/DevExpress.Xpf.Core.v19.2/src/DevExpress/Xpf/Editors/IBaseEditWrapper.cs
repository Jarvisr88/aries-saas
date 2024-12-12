namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Editors.Validation;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    public interface IBaseEditWrapper
    {
        event EditValueChangedEventHandler EditValueChanged;

        void ClearEditorError();
        bool DoValidate();
        void FlushPendingEditActions();
        bool IsActivatingKey(KeyEventArgs e);
        bool IsChildElement(IInputElement element, DependencyObject root = null);
        void LockEditorFocus();
        bool NeedsKey(KeyEventArgs e);
        void ProcessActivatingKey(KeyEventArgs e);
        void ResetEditorCache();
        void SelectAll();
        void SetDisplayTemplate(ControlTemplate template);
        void SetEditTemplate(ControlTemplate template);
        void SetKeyboardFocus();
        void SetValidationError(BaseValidationError validationError);
        void SetValidationErrorTemplate(DataTemplate template);
        void UnlockEditorFocus();

        bool IsReadOnly { get; set; }

        bool ShowEditorButtons { get; set; }

        bool IsEditorActive { get; }

        bool IsValueChanged { get; set; }

        DevExpress.Xpf.Editors.EditMode EditMode { get; set; }

        object EditValue { get; set; }

        HorizontalAlignment HorizontalContentAlignment { get; }

        bool CanHandleBubblingEvent { get; }
    }
}

