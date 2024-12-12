namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Input;

    public class InplaceEditorManager : InplaceEditorManagerBase
    {
        private const string tab = "\t";
        private const int EscCode = 0x1b;

        private void CancelEditInVisibleEditor()
        {
            if (this.IsEditorVisible)
            {
                base.Editor.ClearError();
                this.HideEditor(true);
            }
        }

        private void CancelRowEdit()
        {
        }

        private bool CanShowEditor() => 
            base.Editor.IsKeyboardFocusWithin;

        private void CheckFocus()
        {
        }

        private void CommitEditor(bool closeEditor = false)
        {
            if (this.IsEditorVisible && this.PostEditor())
            {
                this.HideEditor(closeEditor);
            }
        }

        public void HideEditor(bool closeEditor)
        {
            if (this.IsEditorVisible)
            {
                base.Editor.EditMode = EditMode.InplaceInactive;
                base.Editor.EditValue = base.OwnerInfo.GetEditableValue();
                base.Editor.EditValueChanged -= new EditValueChangedEventHandler(this.OnEditValueChanged);
                base.EditorOwner.ActiveEditor = null;
                this.OnHiddenEditor();
            }
            if (closeEditor)
            {
                base.EditorOwner.EditorWasClosed = true;
            }
        }

        private void OnEditValueChanged(object sender, EditValueChangedEventArgs e)
        {
        }

        private void OnHiddenEditor()
        {
            this.UpdateEditorButtonVisibility();
        }

        private void OnShowEditor()
        {
        }

        public override void PreviewKeyDown(KeyEventArgs e)
        {
            if (!this.IsEditorVisible && base.Editor.IsActivatingKey(e.Key, ModifierKeysHelper.GetKeyboardModifiers(e)))
            {
                this.ShowEditor(true);
                if (!this.IsEditorVisible)
                {
                    this.RaiseKeyDownEvent(e);
                }
                else
                {
                    base.EditorOwner.EnqueueImmediateAction(new DelegateAction(() => this.Editor.ProcessActivatingKey(e.Key, ModifierKeysHelper.GetKeyboardModifiers(e))));
                    e.Handled = true;
                }
            }
            else if (!base.Editor.NeedsKey(e.Key, ModifierKeysHelper.GetKeyboardModifiers(e)))
            {
                if (e.Key == Key.Return)
                {
                    if (!this.IsEditorVisible)
                    {
                        this.ShowEditor(true);
                    }
                    else
                    {
                        this.CommitEditor(true);
                        this.CheckFocus();
                    }
                    e.Handled = true;
                }
                if ((e.Key == Key.F2) && !this.IsEditorVisible)
                {
                    this.ShowEditor(true);
                    e.Handled = true;
                }
                if (e.Key == Key.Escape)
                {
                    if (!this.IsEditorVisible)
                    {
                        this.CancelRowEdit();
                    }
                    else
                    {
                        this.CancelEditInVisibleEditor();
                        this.CheckFocus();
                        e.Handled = true;
                    }
                }
                if (!e.Handled)
                {
                    this.RaiseKeyDownEvent(e);
                }
            }
        }

        public override void PreviewTextInput(TextCompositionEventArgs e)
        {
            if (this.ShouldProcessTextInput(e))
            {
                if (!this.IsEditorVisible)
                {
                    this.ShowEditor(true);
                }
                if (this.IsEditorVisible && !base.Editor.IsEditorActive)
                {
                    base.EditorOwner.EnqueueImmediateAction(new DelegateAction(delegate {
                        if (this.OwnerInfo.IsInTree)
                        {
                            Func<TextCompositionEventArgs, TextCompositionEventArgs> cloneFunc = <>c.<>9__2_1;
                            if (<>c.<>9__2_1 == null)
                            {
                                Func<TextCompositionEventArgs, TextCompositionEventArgs> local1 = <>c.<>9__2_1;
                                cloneFunc = <>c.<>9__2_1 = args => new TextCompositionEventArgs(args.Device, args.TextComposition);
                            }
                            ReraiseEventHelper.ReraiseEvent<TextCompositionEventArgs>(e, (UIElement) FocusHelper.GetFocusedElement(), UIElement.PreviewTextInputEvent, UIElement.TextInputEvent, cloneFunc);
                        }
                    }));
                    e.Handled = true;
                }
            }
        }

        private void RaiseKeyDownEvent(KeyEventArgs e)
        {
            e.Handled = true;
            base.EditorOwner.ProcessKeyDown(e);
        }

        private void SetActiveEditMode()
        {
            base.Editor.EditMode = EditMode.InplaceActive;
            base.Editor.EditValue = base.OwnerInfo.GetEditableValue();
        }

        private bool ShouldProcessTextInput(TextCompositionEventArgs e) => 
            (e.Text != "\t") ? (!string.IsNullOrEmpty(e.Text) && (string.IsNullOrEmpty(e.ControlText) && (string.IsNullOrEmpty(e.SystemText) && (e.Text[0] != '\x001b')))) : false;

        public override bool ShowEditor(bool selectAll = false)
        {
            if (!this.CanShowEditor())
            {
                return false;
            }
            if (!this.IsEditorVisible)
            {
                this.UpdateEditTemplate();
                base.Editor.IsReadOnly = this.IsReadOnly;
                this.SetActiveEditMode();
                base.EditorOwner.ActiveEditor = base.Editor;
                base.Editor.Focus();
                this.UpdateEditContext();
                base.Editor.EditValueChanged += new EditValueChangedEventHandler(this.OnEditValueChanged);
            }
            this.OnShowEditor();
            base.EditorOwner.EditorWasClosed = false;
            this.UpdateEditorButtonVisibility();
            BaseEditHelper.SetIsValueChanged(base.Editor, false);
            if (selectAll)
            {
                base.EditorOwner.EnqueueImmediateAction(new DelegateAction(delegate {
                    base.Editor.SelectAll();
                }));
            }
            return true;
        }

        private void UpdateEditContext()
        {
            if (this.HasAccessToCell)
            {
                base.Editor.EditValue = base.OwnerInfo.GetEditableValue();
            }
        }

        private void UpdateEditorButtonVisibility()
        {
            if (base.OwnerInfo.IsInTree)
            {
                base.Editor.ShowEditorButtons = this.IsEditorVisible || base.OwnerInfo.IsInactiveEditorButtonVisible;
            }
        }

        private void UpdateEditTemplate()
        {
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly InplaceEditorManager.<>c <>9 = new InplaceEditorManager.<>c();
            public static Func<TextCompositionEventArgs, TextCompositionEventArgs> <>9__2_1;

            internal TextCompositionEventArgs <PreviewTextInput>b__2_1(TextCompositionEventArgs args) => 
                new TextCompositionEventArgs(args.Device, args.TextComposition);
        }
    }
}

