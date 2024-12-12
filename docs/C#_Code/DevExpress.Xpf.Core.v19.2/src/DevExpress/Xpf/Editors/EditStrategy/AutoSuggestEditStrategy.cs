namespace DevExpress.Xpf.Editors.EditStrategy
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Core.ReflectionExtensions;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Native;
    using DevExpress.Xpf.Editors.Services;
    using DevExpress.Xpf.Editors.Validation.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Input;

    public class AutoSuggestEditStrategy : PopupBaseEditStrategy
    {
        private static readonly IBindingExpressionStatic BindingExpressionWrapper = typeof(BindingExpression).Wrap<IBindingExpressionStatic>();

        public AutoSuggestEditStrategy(AutoSuggestEdit editor) : base(editor)
        {
            this.AcceptPopupValueAction = new PostponedAction(() => this.VisualClient.IsPopupOpened);
        }

        public virtual void AcceptPopupValue(bool force = false)
        {
            if (!this.Editor.IsReadOnly)
            {
                object selectedItem = this.VisualClient.GetSelectedItem();
                if (force)
                {
                    this.AcceptPopupValueInternal(selectedItem);
                }
                else
                {
                    this.AcceptPopupValueAction.PerformPostpone(() => this.AcceptPopupValueInternal(selectedItem));
                }
            }
        }

        protected virtual void AcceptPopupValueInternal(object selectedItem)
        {
            base.EditBox.BeforeAcceptPopupValue();
            object obj2 = this.Editor.RaiseSuggestionChoosing(selectedItem);
            if (obj2 != null)
            {
                base.EditBox.BeforeAcceptPopupValue();
                LookUpEditableItem editValue = this.CalcEditableItem(obj2);
                base.ValueContainer.SetEditValue(editValue, UpdateEditorSource.ValueChanging);
                base.EditBox.AfterAcceptPopupValue();
                this.UpdateDisplayText();
                if (this.PropertyProvider.SelectAllOnAcceptPopup)
                {
                    this.SelectAll();
                }
                this.FlushPopupHighlightedText();
                this.Editor.RaiseSuggestionChosen(selectedItem);
            }
        }

        public virtual LookUpEditableItem CalcEditableItem(object selectedItem)
        {
            object obj2 = this.CalcEditableValue(selectedItem);
            LookUpEditableItem item1 = new LookUpEditableItem();
            item1.EditValue = obj2;
            item1.DisplayValue = obj2;
            item1.AcceptedFromPopup = true;
            return item1;
        }

        private object CalcEditableValue(object value)
        {
            if (string.IsNullOrEmpty(this.Editor.TextMember))
            {
                return value;
            }
            if (value is string)
            {
                return value;
            }
            BindingExpression element = this.PrepareItemValueBinding(value, this.Editor.TextMember);
            if (element == null)
            {
                return value;
            }
            IBindingExpressionInstance instance = element.Wrap<IBindingExpressionInstance>();
            instance.Activate(value);
            object obj2 = instance.Value;
            instance.Deactivate();
            return obj2;
        }

        private void ClearItemValueBinding()
        {
            BindingExpressionUncommonField.ClearValue(this.Editor);
        }

        protected internal override object ConvertEditValueForFormatDisplayText(object editValue)
        {
            LookUpEditableItem item = editValue as LookUpEditableItem;
            return ((item != null) ? item.DisplayValue : editValue);
        }

        protected override EditorSpecificValidator CreateEditorValidatorService() => 
            new AutoSuggestEditValidator(this.Editor);

        public virtual void DisplayMemberChanged(string newValue)
        {
        }

        private void FlushPopupHighlightedText()
        {
            this.Editor.PopupHighlightedText = null;
        }

        public virtual bool IsClosePopupWithAcceptGesture(Key key, ModifierKeys modifiers) => 
            this.VisualClient.IsClosePopupWithAcceptGesture(key, modifiers);

        public virtual bool IsClosePopupWithCancelGesture(Key key, ModifierKeys modifiers) => 
            this.VisualClient.IsClosePopupWithCancelGesture(key, modifiers);

        public virtual void ItemsSourceChanged(object newValue)
        {
        }

        public override void OnLostFocus()
        {
            base.OnLostFocus();
            this.FlushPopupHighlightedText();
        }

        public virtual void PopupDestroyed()
        {
            this.AcceptPopupValueAction.PerformForce();
            this.FlushPopupHighlightedText();
        }

        private BindingExpression PrepareItemValueBinding(object item, string member)
        {
            if (item == null)
            {
                return null;
            }
            BindingExpression bindingExpr = BindingExpressionUncommonField.GetValue(this.Editor);
            if (bindingExpr == null)
            {
                Binding binding = new Binding {
                    Source = null,
                    Path = new PropertyPath(member, new object[0])
                };
                bindingExpr = (BindingExpression) BindingExpressionWrapper.CreateUntargetedBindingExpression(this.Editor, binding);
                BindingExpressionUncommonField.SetValue(this.Editor, bindingExpr);
            }
            return bindingExpr;
        }

        protected override void ProcessKeyDownInternal(KeyEventArgs e)
        {
            base.ProcessKeyDownInternal(e);
            this.VisualClient.ProcessKeyDown(e);
        }

        private void ProcessPopupHighlightedText()
        {
            if (this.Editor.AllowPopupTextHighlighting)
            {
                this.Editor.PopupHighlightedText = this.Editor.RaiseCustomPopupHighlightedText(base.EditBox.Text);
            }
            else
            {
                this.FlushPopupHighlightedText();
            }
        }

        protected override void ProcessPreviewKeyDownInternal(KeyEventArgs e)
        {
            base.ProcessPreviewKeyDownInternal(e);
            this.VisualClient.ProcessPreviewKeyDown(e);
        }

        protected internal override bool? RestoreDisplayText()
        {
            bool? nullable = base.RestoreDisplayText();
            bool? nullable2 = nullable;
            bool flag = true;
            if ((nullable2.GetValueOrDefault() == flag) ? (nullable2 != null) : false)
            {
                this.Editor.RaiseQuerySubmitted(base.EditBox.Text);
            }
            return nullable;
        }

        private void ShowImmediatePopup()
        {
            if (this.Editor.ImmediatePopup && !string.IsNullOrEmpty(base.EditBox.Text))
            {
                this.Editor.ShowPopup();
            }
        }

        protected override void SyncWithEditorInternal()
        {
            base.SyncWithEditorInternal();
            this.ProcessPopupHighlightedText();
            this.ShowImmediatePopup();
        }

        public virtual void TextMemberChanged(string newValue)
        {
            this.ClearItemValueBinding();
            this.UpdateDisplayText();
        }

        protected internal override void ValidateOnEnterKeyPressed(KeyEventArgs e)
        {
            if (!this.VisualClient.PostPopupValue)
            {
                base.ValidateOnEnterKeyPressed(e);
            }
        }

        private AutoSuggestEditPropertyProvider PropertyProvider =>
            base.PropertyProvider as AutoSuggestEditPropertyProvider;

        protected SelectorVisualClientOwner VisualClient =>
            this.Editor.VisualClient as SelectorVisualClientOwner;

        private AutoSuggestEdit Editor =>
            base.Editor as AutoSuggestEdit;

        private PostponedAction AcceptPopupValueAction { get; set; }
    }
}

