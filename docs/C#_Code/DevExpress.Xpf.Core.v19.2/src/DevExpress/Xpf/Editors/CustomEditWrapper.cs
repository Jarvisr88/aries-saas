namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.Validation;
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;

    public class CustomEditWrapper : IBaseEditWrapper
    {
        private readonly InplaceEditorBase inplaceEditor;
        private bool isValueChanged;
        private EditMode editMode = EditMode.InplaceInactive;
        private object editValue;

        event EditValueChangedEventHandler IBaseEditWrapper.EditValueChanged
        {
            add
            {
            }
            remove
            {
            }
        }

        public CustomEditWrapper(InplaceEditorBase inplaceEditor)
        {
            this.inplaceEditor = inplaceEditor;
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
            LayoutHelper.IsChildElementEx(root, element as DependencyObject, true);

        void IBaseEditWrapper.LockEditorFocus()
        {
        }

        bool IBaseEditWrapper.NeedsKey(KeyEventArgs e) => 
            ReferenceEquals(PresentationSource.FromDependencyObject(this.inplaceEditor), PresentationSource.FromDependencyObject((DependencyObject) Keyboard.FocusedElement)) ? ((e.Key != Key.Escape) && ((e.Key != Key.Return) && ((e.Key != Key.F2) && (e.Key != Key.Tab)))) : true;

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
            this.RefreshFocus();
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

        private UIElement FindFocusableElement(UIElement current)
        {
            if (current != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(current); i++)
                {
                    UIElement child = VisualTreeHelper.GetChild(current, i) as UIElement;
                    if ((child != null) && child.Focusable)
                    {
                        return child;
                    }
                    child = this.FindFocusableElement(child);
                    if (child != null)
                    {
                        return child;
                    }
                }
            }
            return null;
        }

        private void RefreshFocus()
        {
            if (this.editMode == EditMode.InplaceInactive)
            {
                this.inplaceEditor.Focus();
            }
            else
            {
                UIElement element = this.FindFocusableElement(this.inplaceEditor);
                if (element != null)
                {
                    element.Focus();
                }
            }
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
            this.editMode == EditMode.InplaceActive;

        bool IBaseEditWrapper.IsValueChanged
        {
            get => 
                this.isValueChanged;
            set => 
                this.isValueChanged = value;
        }

        EditMode IBaseEditWrapper.EditMode
        {
            get => 
                this.editMode;
            set
            {
                if (this.editMode != value)
                {
                    this.editMode = value;
                    if (this.inplaceEditor.IsKeyboardFocusWithin)
                    {
                        this.RefreshFocus();
                    }
                }
            }
        }

        object IBaseEditWrapper.EditValue
        {
            get => 
                this.editValue;
            set
            {
                this.isValueChanged = true;
                this.editValue = value;
            }
        }

        HorizontalAlignment IBaseEditWrapper.HorizontalContentAlignment =>
            HorizontalAlignment.Left;

        bool IBaseEditWrapper.CanHandleBubblingEvent =>
            this.editMode != EditMode.InplaceActive;
    }
}

