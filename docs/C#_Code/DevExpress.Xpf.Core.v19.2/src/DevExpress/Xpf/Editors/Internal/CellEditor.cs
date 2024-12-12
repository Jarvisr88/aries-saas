namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;

    public class CellEditor : InplaceEditorBase
    {
        public static readonly DependencyProperty OwnerTokenEditorProperty;
        public static readonly DependencyProperty IsEditorFocusedProperty;
        public static readonly DependencyProperty IsTokenFocusedProperty;
        public static readonly DependencyProperty ItemDataProperty;

        static CellEditor()
        {
            Type ownerType = typeof(CellEditor);
            OwnerTokenEditorProperty = DependencyProperty.Register("OwnerTokenEditor", typeof(TokenEditor), ownerType, new FrameworkPropertyMetadata((d, e) => ((CellEditor) d).OnTokenEditorChanged(e.OldValue as TokenEditor)));
            IsEditorFocusedProperty = DependencyProperty.Register("IsEditorFocused", typeof(bool), ownerType, new FrameworkPropertyMetadata((d, e) => ((CellEditor) d).OnIsEditorFocusedChanged()));
            IsTokenFocusedProperty = DependencyProperty.Register("IsTokenFocused", typeof(bool), ownerType, new FrameworkPropertyMetadata((d, e) => ((CellEditor) d).OnIsTokenFocusedChanged()));
            ItemDataProperty = DependencyProperty.Register("ItemData", typeof(TokenItemData), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure, (d, e) => ((CellEditor) d).OnItemDataChanged(e.OldValue as TokenItemData)));
        }

        public override void CancelEditInVisibleEditor()
        {
            this.OwnerTokenEditor.BeforeCancelEdit();
            base.CancelEditInVisibleEditor();
        }

        public override bool CanShowEditor() => 
            this.OwnerTokenEditor.OwnerEdit.EditMode != EditMode.InplaceInactive;

        protected override IBaseEdit CreateEditor(BaseEditSettings settings)
        {
            IBaseEdit editor = settings.CreateEditor(this.EditorColumn, EditorOptimizationMode.Disabled);
            this.SetupEditor(editor);
            return editor;
        }

        private void EditorCommandPreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (ReferenceEquals(e.Command, ApplicationCommands.Paste))
            {
                e.Handled = this.OwnerTokenEditor.OnCellEditorPaste();
            }
        }

        internal void FocusEditCore()
        {
            if (base.editCore != null)
            {
                base.editCore.Focus();
            }
        }

        protected override object GetEditableValue() => 
            this.ItemData?.DisplayText;

        internal IBaseEdit GetEditor() => 
            BaseEditHelper.GetBaseEdit(base.editCore);

        protected override EditableDataObject GetEditorDataContext() => 
            this.ItemData;

        internal IBaseEdit GetInactiveEditor() => 
            base.editCore;

        protected override void InitializeBaseEdit(IBaseEdit newEdit, InplaceEditorBase.BaseEditSourceType newBaseEditSourceType)
        {
            base.InitializeBaseEdit(newEdit, newBaseEditSourceType);
            this.ShowEditorButtons(this.PresenterOwner.ShowButtons);
        }

        public bool IsActivatingKey(Key key, ModifierKeys modifiers) => 
            (base.editCore != null) && (!this.IsEditorFocused && (base.editCore.NeedsKey(key, modifiers) && ModifierKeysHelper.NoModifiers(modifiers)));

        protected override bool IsInactiveEditorButtonVisible() => 
            true;

        private bool IsInEditingMode() => 
            (this.OwnerTokenEditor != null) && this.OwnerTokenEditor.HasActiveEditor;

        protected override void OnEditorActivated()
        {
            base.OnEditorActivated();
            this.UpdateActiveEditor();
            this.ShowEditorButtons(false);
        }

        protected override void OnEditorActivated(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            base.OnEditorActivated(sender, e);
        }

        protected override void OnEditorPreviewLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (!this.OwnerTokenEditor.IsEditorKeyboardFocused)
            {
                base.OnEditorPreviewLostKeyboardFocus(sender, e);
            }
        }

        protected override void OnEditValueChanged(object sender, EditValueChangedEventArgs e)
        {
            base.OnEditValueChanged(sender, e);
            this.OwnerTokenEditor.ProcessActiveEditorEditValueChanged((string) e.OldValue, (string) e.NewValue);
        }

        protected override void OnHiddenEditor(bool closeEditor)
        {
            base.OnHiddenEditor(closeEditor);
            this.OwnerTokenEditor.OnTokenHidden();
            this.ShowEditorButtons(this.PresenterOwner.ShowButtons);
        }

        private void OnIsEditorFocusedChanged()
        {
            base.OnIsFocusedCellChanged();
            this.ShowEditorButtons(!this.IsEditorFocused);
        }

        private void OnIsTokenFocusedChanged()
        {
        }

        private void OnItemDataChanged(TokenItemData oldData)
        {
            if (this.IsInEditingMode())
            {
                this.UpdateEditValueInEditingMode();
            }
            else
            {
                if (this.ItemData == null)
                {
                    this.SetEdit(null);
                }
                Func<TokenItemData, DevExpress.Xpf.Editors.Internal.EditorColumn> evaluator = <>c.<>9__55_0;
                if (<>c.<>9__55_0 == null)
                {
                    Func<TokenItemData, DevExpress.Xpf.Editors.Internal.EditorColumn> local1 = <>c.<>9__55_0;
                    evaluator = <>c.<>9__55_0 = x => x.Column;
                }
                this.OnOwnerChanged(oldData.Return<TokenItemData, DevExpress.Xpf.Editors.Internal.EditorColumn>(evaluator, <>c.<>9__55_1 ??= ((Func<DevExpress.Xpf.Editors.Internal.EditorColumn>) (() => null))));
                this.UpdateEditCoreEditValue();
            }
        }

        private void OnTokenEditorChanged(TokenEditor oldValue)
        {
            this.OnOwnerChanged(this.ItemData?.Column);
        }

        protected override bool PostEditorCore() => 
            true;

        protected override void ProcessMouseEventInInplaceInactiveMode(MouseButtonEventArgs e)
        {
            base.ProcessMouseEventInInplaceInactiveMode(e);
        }

        internal void Refresh()
        {
            base.OnIsFocusedCellChanged();
        }

        internal void SetPresenterOwner(TokenEditorPresenter owner)
        {
            this.PresenterOwner = owner;
        }

        private void SetupEditor(IBaseEdit editor)
        {
            ButtonEdit edit = editor as ButtonEdit;
            if (edit != null)
            {
                edit.IsTabStop = false;
                edit.ShowEditorButtons = this.PresenterOwner.ShowButtons;
                edit.Style = this.PresenterOwner.ActiveEditorStyle;
                edit.IsReadOnly = this.IsReadOnly;
                edit.SelectAllOnGotFocus = true;
                edit.CharacterCasing = this.OwnerTokenEditor.CharacterCasing;
                edit.ShowTooltipForTrimmedText = false;
                edit.MaxLength = this.OwnerTokenEditor.MaxTextLength;
                edit.AddHandler(CommandManager.PreviewExecutedEvent, new ExecutedRoutedEventHandler(this.EditorCommandPreviewExecuted));
            }
        }

        private void ShowEditorButtons(bool value)
        {
            Action<IBaseEdit> action = <>c.<>9__54_0;
            if (<>c.<>9__54_0 == null)
            {
                Action<IBaseEdit> local1 = <>c.<>9__54_0;
                action = <>c.<>9__54_0 = x => x.ShowEditorButtons = true;
            }
            this.GetEditor().Do<IBaseEdit>(action);
        }

        private void UpdateActiveEditor()
        {
            this.OwnerTokenEditor.OnStartEditing(this.GetEditor() as ButtonEdit);
        }

        internal void UpdateEditCoreEditValue()
        {
            base.UpdateEditValue(base.editCore);
        }

        protected override void UpdateEditValueCore(IBaseEdit editor)
        {
            if ((editor != null) && (this.ItemData != null))
            {
                editor.EditValue = this.ItemData.DisplayText;
                Action<UIElement> action = <>c.<>9__45_0;
                if (<>c.<>9__45_0 == null)
                {
                    Action<UIElement> local1 = <>c.<>9__45_0;
                    action = <>c.<>9__45_0 = x => x.InvalidateMeasure();
                }
                (base.editCore as UIElement).Do<UIElement>(action);
            }
        }

        private void UpdateEditValueInEditingMode()
        {
            this.UpdateEditCoreEditValue();
        }

        public override void ValidateEditorCore()
        {
            base.ValidateEditorCore();
            base.Edit.DoValidate();
        }

        public TokenEditor OwnerTokenEditor
        {
            get => 
                (TokenEditor) base.GetValue(OwnerTokenEditorProperty);
            set => 
                base.SetValue(OwnerTokenEditorProperty, value);
        }

        public bool IsEditorFocused
        {
            get => 
                (bool) base.GetValue(IsEditorFocusedProperty);
            set => 
                base.SetValue(IsEditorFocusedProperty, value);
        }

        public bool IsTokenFocused
        {
            get => 
                (bool) base.GetValue(IsTokenFocusedProperty);
            set => 
                base.SetValue(IsTokenFocusedProperty, value);
        }

        public TokenItemData ItemData
        {
            get => 
                (TokenItemData) base.GetValue(ItemDataProperty);
            set => 
                base.SetValue(ItemDataProperty, value);
        }

        private TokenEditorPresenter PresenterOwner { get; set; }

        protected override InplaceEditorOwnerBase Owner =>
            this.OwnerTokenEditor?.CellEditorOwner;

        protected override IInplaceEditorColumn EditorColumn =>
            this.ItemData?.Column;

        protected override bool IsCellFocused =>
            this.IsEditorFocused;

        protected override bool IsReadOnly =>
            (this.OwnerTokenEditor != null) ? this.OwnerTokenEditor.IsReadOnly : false;

        protected override bool OverrideCellTemplate =>
            false;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CellEditor.<>c <>9 = new CellEditor.<>c();
            public static Action<UIElement> <>9__45_0;
            public static Action<IBaseEdit> <>9__54_0;
            public static Func<TokenItemData, EditorColumn> <>9__55_0;
            public static Func<EditorColumn> <>9__55_1;

            internal void <.cctor>b__4_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((CellEditor) d).OnTokenEditorChanged(e.OldValue as TokenEditor);
            }

            internal void <.cctor>b__4_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((CellEditor) d).OnIsEditorFocusedChanged();
            }

            internal void <.cctor>b__4_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((CellEditor) d).OnIsTokenFocusedChanged();
            }

            internal void <.cctor>b__4_3(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((CellEditor) d).OnItemDataChanged(e.OldValue as TokenItemData);
            }

            internal EditorColumn <OnItemDataChanged>b__55_0(TokenItemData x) => 
                x.Column;

            internal EditorColumn <OnItemDataChanged>b__55_1() => 
                null;

            internal void <ShowEditorButtons>b__54_0(IBaseEdit x)
            {
                x.ShowEditorButtons = true;
            }

            internal void <UpdateEditValueCore>b__45_0(UIElement x)
            {
                x.InvalidateMeasure();
            }
        }
    }
}

