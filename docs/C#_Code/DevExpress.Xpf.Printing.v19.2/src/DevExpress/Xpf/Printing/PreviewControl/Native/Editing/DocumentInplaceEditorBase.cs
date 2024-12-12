namespace DevExpress.Xpf.Printing.PreviewControl.Native.Editing
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Validation;
    using DevExpress.Xpf.Printing;
    using DevExpress.XtraPrinting;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;

    public abstract class DocumentInplaceEditorBase : InplaceEditorBase
    {
        private readonly Locker locker = new Locker();
        private DocumentInplaceEditorOwner owner;
        private readonly IInplaceEditorColumn column;

        protected DocumentInplaceEditorBase(DocumentInplaceEditorOwner owner, IInplaceEditorColumn column, DevExpress.XtraPrinting.EditingField editingField)
        {
            base.EditorSourceType = InplaceEditorBase.BaseEditSourceType.CellTemplate;
            this.owner = owner;
            this.column = column;
            this.EditingField = editingField;
            this.InitialData = this.CreateInitialData();
            this.Grid = new System.Windows.Controls.Grid();
            this.Border = new System.Windows.Controls.Border();
            this.Grid.Children.Add(this.Border);
            this.Border.Child = this;
            owner.VisualHost = this.Grid;
            base.OnOwnerChanged(null);
            base.Focusable = false;
        }

        public virtual bool CommitEditor(bool closeEditor = false)
        {
            if (!this.IsEditorVisible)
            {
                return true;
            }
            bool flag = this.PostEditor(true);
            if (flag)
            {
                base.HideEditor(closeEditor);
            }
            return flag;
        }

        protected abstract InplaceEditorInitialData CreateInitialData();
        protected override object GetEditableValue() => 
            this.InitialData.Value;

        protected override EditableDataObject GetEditorDataContext() => 
            this.InitialData;

        protected override bool IsInactiveEditorButtonVisible() => 
            false;

        protected override void OnHiddenEditor(bool closeEditor)
        {
            base.OnHiddenEditor(closeEditor);
            this.DocumentPresenter.EditingStrategy.EndEditing();
            this.DocumentPresenter.DetachEditorFromTree();
            this.DocumentPresenter.Focus();
        }

        protected abstract override bool PostEditorCore();
        internal void ReleaseOwner()
        {
            this.owner = null;
        }

        protected override void SetEdit(IBaseEdit value)
        {
            base.SetEdit(value);
            IBaseEditWrapper editWrapper = base.editWrapper;
            if (base.editWrapper == null)
            {
                IBaseEditWrapper local1 = base.editWrapper;
                editWrapper = new FakeEditWrapper(this, this.InitialData);
            }
            this.editWrapper = editWrapper;
        }

        protected override void UpdateEditValueCore(IBaseEdit editor)
        {
            this.locker.DoLockedAction<object>(delegate {
                object obj2;
                editor.EditValue = obj2 = this.GetEditableValue();
                return obj2;
            });
        }

        private System.Windows.Controls.Grid Grid { get; set; }

        protected System.Windows.Controls.Border Border { get; set; }

        protected InplaceEditorInitialData InitialData { get; private set; }

        public DevExpress.XtraPrinting.EditingField EditingField { get; private set; }

        protected override IInplaceEditorColumn EditorColumn =>
            this.column;

        protected override bool IsCellFocused =>
            true;

        protected override bool IsReadOnly =>
            false;

        protected override bool OverrideCellTemplate =>
            false;

        protected override InplaceEditorOwnerBase Owner =>
            this.owner;

        protected DocumentPresenterControl DocumentPresenter =>
            this.owner.Presenter;

        protected class FakeEditWrapper : IBaseEditWrapper
        {
            private readonly DocumentInplaceEditorBase.InplaceEditorInitialData initialData;
            private readonly DocumentInplaceEditorBase editor;

            event EditValueChangedEventHandler IBaseEditWrapper.EditValueChanged
            {
                add
                {
                }
                remove
                {
                }
            }

            public FakeEditWrapper(DocumentInplaceEditorBase editor, DocumentInplaceEditorBase.InplaceEditorInitialData initialData)
            {
                this.initialData = initialData;
                this.editor = editor;
                editor.Loaded += new RoutedEventHandler(this.Editor_Loaded);
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

            bool IBaseEditWrapper.NeedsKey(KeyEventArgs e)
            {
                Key key = e.Key;
                ModifierKeys keyboardModifiers = ModifierKeysHelper.GetKeyboardModifiers(e);
                bool? nullable = this.NeedsBasicKey(key, () => this.NeedsEnter());
                return ((nullable == null) ? ((key != Key.Tab) ? (((key == Key.Left) || (key == Key.Right)) ? this.NeedsLeftRight() : (((key == Key.Up) || (key == Key.Down)) ? this.NeedsUpDown() : true)) : this.NeedsTab()) : nullable.Value);
            }

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

            private void Editor_Loaded(object sender, RoutedEventArgs e)
            {
                this.editor.Loaded -= new RoutedEventHandler(this.Editor_Loaded);
                if (VisualTreeHelper.GetChildrenCount(this.editor) > 0)
                {
                    Action<FrameworkElement> action = <>c.<>9__3_0;
                    if (<>c.<>9__3_0 == null)
                    {
                        Action<FrameworkElement> local1 = <>c.<>9__3_0;
                        action = <>c.<>9__3_0 = x => x.Focus();
                    }
                    (VisualTreeHelper.GetChild(this.editor, 0) as FrameworkElement).Do<FrameworkElement>(action);
                }
            }

            public static Key GetKey(KeyEventArgs e) => 
                (e.Key == Key.System) ? e.SystemKey : e.Key;

            private bool? NeedsBasicKey(Key key, Func<bool> needsEnterFunc)
            {
                if ((key == Key.Escape) || (key == Key.F2))
                {
                    return false;
                }
                if (key == Key.Return)
                {
                    return new bool?(needsEnterFunc());
                }
                return null;
            }

            private bool NeedsEnter() => 
                false;

            private bool NeedsLeftRight() => 
                true;

            private bool NeedsTab() => 
                false;

            private bool NeedsUpDown() => 
                true;

            bool IBaseEditWrapper.IsReadOnly
            {
                get => 
                    false;
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
                true;

            bool IBaseEditWrapper.IsValueChanged
            {
                get => 
                    this.initialData.IsValueChanged;
                set
                {
                }
            }

            EditMode IBaseEditWrapper.EditMode
            {
                get => 
                    EditMode.InplaceActive;
                set
                {
                }
            }

            object IBaseEditWrapper.EditValue
            {
                get => 
                    this.initialData.Value;
                set
                {
                }
            }

            HorizontalAlignment IBaseEditWrapper.HorizontalContentAlignment =>
                HorizontalAlignment.Left;

            bool IBaseEditWrapper.CanHandleBubblingEvent =>
                true;

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly DocumentInplaceEditorBase.FakeEditWrapper.<>c <>9 = new DocumentInplaceEditorBase.FakeEditWrapper.<>c();
                public static Action<FrameworkElement> <>9__3_0;

                internal void <Editor_Loaded>b__3_0(FrameworkElement x)
                {
                    x.Focus();
                }
            }
        }

        protected class InplaceEditorInitialData : EditableDataObject
        {
            private readonly object initialValue;

            protected InplaceEditorInitialData(object initialValue)
            {
                this.initialValue = initialValue;
                base.Value = initialValue;
            }

            public virtual bool IsValueChanged =>
                base.Value != this.initialValue;
        }
    }
}

