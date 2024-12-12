namespace DevExpress.Xpf.Editors.Native
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Helpers;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class TestInplaceEditor : InplaceEditorBase
    {
        public static readonly DependencyProperty IsEditorFocusedProperty;
        private readonly TestInplaceEditorOwner owner;
        private readonly TestInplaceEditorColumn column;

        static TestInplaceEditor()
        {
            IsEditorFocusedProperty = DependencyProperty.Register("IsEditorFocused", typeof(bool), typeof(TestInplaceEditor), new UIPropertyMetadata(false, (d, e) => ((TestInplaceEditor) d).OnIsFocusedCellChanged()));
        }

        public TestInplaceEditor(TestInplaceEditorOwner owner, TestInplaceEditorColumn column)
        {
            this.owner = owner;
            this.column = column;
            base.OnOwnerChanged(null);
        }

        protected override object GetEditableValue() => 
            this.Data.Value;

        protected override EditableDataObject GetEditorDataContext() => 
            this.Data;

        protected override EditorOptimizationMode GetEditorOptimizationMode() => 
            EditorOptimizationMode.Simple;

        protected override bool IsInactiveEditorButtonVisible() => 
            false;

        protected override void OnHiddenEditor(bool closeEditor)
        {
            base.OnHiddenEditor(closeEditor);
            base.EditableValue = this.GetEditableValue();
        }

        protected override void OnInnerContentChangedCore()
        {
            base.OnInnerContentChangedCore();
            this.UpdateDisplayTemplate(false);
        }

        protected override bool PostEditorCore()
        {
            this.Data.Value = base.EditableValue;
            return true;
        }

        protected override void UpdateEditValueCore(IBaseEdit editor)
        {
            editor.EditValue = this.GetEditableValue();
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
            public static readonly TestInplaceEditor.<>c <>9 = new TestInplaceEditor.<>c();

            internal void <.cctor>b__27_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((TestInplaceEditor) d).OnIsFocusedCellChanged();
            }
        }
    }
}

