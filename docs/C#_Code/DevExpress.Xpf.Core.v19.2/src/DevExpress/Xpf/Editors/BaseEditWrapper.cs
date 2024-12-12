namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Validation;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    public class BaseEditWrapper : IBaseEditWrapper
    {
        private readonly IBaseEdit edit;

        event EditValueChangedEventHandler IBaseEditWrapper.EditValueChanged
        {
            add
            {
                this.edit.EditValueChanged += value;
            }
            remove
            {
                this.edit.EditValueChanged -= value;
            }
        }

        public BaseEditWrapper(IBaseEdit edit)
        {
            this.edit = edit;
        }

        void IBaseEditWrapper.ClearEditorError()
        {
            this.edit.ClearError();
        }

        bool IBaseEditWrapper.DoValidate() => 
            this.edit.DoValidate();

        void IBaseEditWrapper.FlushPendingEditActions()
        {
            BaseEditHelper.FlushPendingEditActions(this.edit);
        }

        bool IBaseEditWrapper.IsActivatingKey(KeyEventArgs e) => 
            BaseEditHelper.GetIsActivatingKey(this.edit, e);

        bool IBaseEditWrapper.IsChildElement(IInputElement element, DependencyObject root) => 
            this.edit.IsChildElement(element, root);

        void IBaseEditWrapper.LockEditorFocus()
        {
            this.edit.CanAcceptFocus = false;
        }

        bool IBaseEditWrapper.NeedsKey(KeyEventArgs e) => 
            BaseEditHelper.GetNeedsKey(this.edit, e);

        void IBaseEditWrapper.ProcessActivatingKey(KeyEventArgs e)
        {
            BaseEditHelper.ProcessActivatingKey(this.edit, e);
        }

        void IBaseEditWrapper.ResetEditorCache()
        {
            BaseEditHelper.ResetEditorCache(this.edit);
        }

        void IBaseEditWrapper.SelectAll()
        {
            this.edit.SelectAll();
        }

        void IBaseEditWrapper.SetDisplayTemplate(ControlTemplate template)
        {
            if (!(this.edit is IInplaceBaseEdit) && (template == null))
            {
                this.edit.ClearValue(BaseEdit.DisplayTemplateProperty);
            }
            else
            {
                this.edit.DisplayTemplate = template;
            }
        }

        void IBaseEditWrapper.SetEditTemplate(ControlTemplate template)
        {
            if (!(this.edit is IInplaceBaseEdit) && (template == null))
            {
                this.edit.ClearValue(BaseEdit.EditTemplateProperty);
            }
            else
            {
                this.edit.EditTemplate = template;
            }
        }

        void IBaseEditWrapper.SetKeyboardFocus()
        {
            KeyboardHelper.Focus((UIElement) this.edit);
        }

        void IBaseEditWrapper.SetValidationError(BaseValidationError validationError)
        {
            BaseEditHelper.SetValidationError((DependencyObject) this.edit, validationError);
        }

        void IBaseEditWrapper.SetValidationErrorTemplate(DataTemplate template)
        {
            BaseEditHelper.SetValidationErrorTemplate((DependencyObject) this.edit, template);
        }

        void IBaseEditWrapper.UnlockEditorFocus()
        {
            this.edit.CanAcceptFocus = true;
        }

        bool IBaseEditWrapper.IsReadOnly
        {
            get => 
                this.edit.IsReadOnly;
            set => 
                this.edit.IsReadOnly = value;
        }

        bool IBaseEditWrapper.IsEditorActive =>
            this.edit.IsEditorActive;

        EditMode IBaseEditWrapper.EditMode
        {
            get => 
                this.edit.EditMode;
            set => 
                this.edit.EditMode = value;
        }

        object IBaseEditWrapper.EditValue
        {
            get => 
                this.edit.EditValue;
            set => 
                this.edit.EditValue = value;
        }

        bool IBaseEditWrapper.ShowEditorButtons
        {
            get => 
                BaseEditHelper.GetShowEditorButtons(this.edit);
            set => 
                BaseEditHelper.SetShowEditorButtons(this.edit, value);
        }

        bool IBaseEditWrapper.IsValueChanged
        {
            get => 
                BaseEditHelper.GetIsValueChanged(this.edit);
            set => 
                BaseEditHelper.SetIsValueChanged(this.edit, value);
        }

        HorizontalAlignment IBaseEditWrapper.HorizontalContentAlignment =>
            this.edit.HorizontalContentAlignment;

        bool IBaseEditWrapper.CanHandleBubblingEvent =>
            true;
    }
}

