namespace DevExpress.Xpf.Editors.Filtering
{
    using DevExpress.Data.Filtering;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Settings;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class InplaceFilterEditor : InplaceEditorBase
    {
        public static readonly DependencyProperty IsEditorFocusedProperty;
        private readonly InplaceFilterEditorOwner owner;
        private readonly InplaceFilterEditorColumn column;

        static InplaceFilterEditor()
        {
            IsEditorFocusedProperty = DependencyPropertyManager.Register("IsEditorFocused", typeof(bool), typeof(InplaceFilterEditor), new PropertyMetadata(false, (d, e) => ((InplaceFilterEditor) d).OnIsFocusedCellChanged()));
        }

        public InplaceFilterEditor(InplaceFilterEditorOwner owner, InplaceFilterEditorColumn column, ClauseNode node)
        {
            this.SetDefaultStyleKey(typeof(InplaceFilterEditor));
            this.SetContentAlignment();
            this.Node = node;
            this.owner = owner;
            this.column = column;
            column.SetDisplayTextIsEmpty(new DisplayTextIsEmptyDelegate(this.DisplayTextIsEmpty));
            base.OnOwnerChanged(null);
        }

        protected override void CheckFocus()
        {
            base.Focus();
        }

        private void ClearEditorValidation(IBaseEdit editor)
        {
            if (editor is SpinEdit)
            {
                decimal? nullable = null;
                (editor as SpinEdit).MinValue = nullable;
                nullable = null;
                (editor as SpinEdit).MaxValue = nullable;
            }
            if (editor is DateEdit)
            {
                DateTime? nullable2 = null;
                (editor as DateEdit).MinValue = nullable2;
                nullable2 = null;
                (editor as DateEdit).MaxValue = nullable2;
            }
        }

        protected override IBaseEdit CreateEditor(BaseEditSettings settings)
        {
            IBaseEdit editor = base.CreateEditor(settings);
            this.ClearEditorValidation(editor);
            if (editor is TextEdit)
            {
                (editor as TextEdit).ShowTooltipForTrimmedText = false;
            }
            ShowValueEditorEventArgs arg = new ShowValueEditorEventArgs((BaseEdit) editor, this.Node, new OperandValue(this.Data.Value));
            ((FilterControl) this.Owner.OwnerElement).RaiseBeforeShowValueEditor(arg);
            if (arg.CustomEditSettings != null)
            {
                editor = base.CreateEditor(arg.CustomEditSettings);
            }
            editor.ShouldDisableExcessiveUpdatesInInplaceInactiveMode = false;
            return editor;
        }

        private bool DisplayTextIsEmpty() => 
            (base.editCore is BaseEdit) && (((BaseEdit) base.editCore).DisplayText == string.Empty);

        protected override object GetEditableValue() => 
            this.Data.Value;

        protected override EditableDataObject GetEditorDataContext() => 
            this.Data;

        protected override bool IsInactiveEditorButtonVisible() => 
            false;

        protected override void OnHiddenEditor(bool closeEditor)
        {
            base.OnHiddenEditor(closeEditor);
            base.EditableValue = this.GetEditableValue();
        }

        protected override bool PostEditorCore()
        {
            this.Data.Value = base.EditableValue;
            this.UpdateDisplayTemplate(false);
            return true;
        }

        internal void SetActiveForTest()
        {
            this.SetActiveEditMode();
        }

        private void SetContentAlignment()
        {
        }

        protected override void UpdateEditValueCore(IBaseEdit editor)
        {
            editor.EditValue = this.GetEditableValue();
        }

        public override void ValidateEditorCore()
        {
            base.ValidateEditorCore();
            base.Edit.DoValidate();
        }

        public bool IsEditorFocused
        {
            get => 
                (bool) base.GetValue(IsEditorFocusedProperty);
            set => 
                base.SetValue(IsEditorFocusedProperty, value);
        }

        internal EditableDataObject Data =>
            this.column.Data;

        private ClauseNode Node { get; set; }

        protected override bool IsCellFocused =>
            this.IsEditorFocused;

        protected override InplaceEditorOwnerBase Owner =>
            this.owner;

        protected override IInplaceEditorColumn EditorColumn =>
            this.column;

        protected override bool IsReadOnly =>
            false;

        protected override bool OverrideCellTemplate =>
            true;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly InplaceFilterEditor.<>c <>9 = new InplaceFilterEditor.<>c();

            internal void <.cctor>b__36_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((InplaceFilterEditor) d).OnIsFocusedCellChanged();
            }
        }
    }
}

