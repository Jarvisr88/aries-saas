namespace DevExpress.Xpf.Printing.PreviewControl.Native.Editing
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Editors;
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Forms;

    public class CheckInplaceEditor : DocumentInplaceEditorBase
    {
        private readonly Func<CheckState, CheckState> getNewValue;

        public CheckInplaceEditor(DocumentInplaceEditorOwner owner, CheckInplaceEditorColumn column) : base(owner, column, column.EditingField)
        {
            Func<CheckState, CheckState> func1 = <>c.<>9__7_0;
            if (<>c.<>9__7_0 == null)
            {
                Func<CheckState, CheckState> local1 = <>c.<>9__7_0;
                func1 = <>c.<>9__7_0 = delegate (CheckState state) {
                    CheckState[] first = new CheckState[2];
                    first[1] = CheckState.Checked;
                    return first.Concat<CheckState>(CheckState.Unchecked.Yield<CheckState>()).SkipWhile<CheckState>(_ => (_ != state)).Skip<CheckState>(1).First<CheckState>();
                };
            }
            this.getNewValue = func1;
            base.Opacity = 0.0;
        }

        protected override DocumentInplaceEditorBase.InplaceEditorInitialData CreateInitialData()
        {
            if (string.IsNullOrEmpty(this.EditingField.GroupID))
            {
                return new CheckBoxEditorInitialData(this.EditingField, new Action(base.DocumentPresenter.Update));
            }
            return new RadioCheckBoxEditorInitialData(this.EditingField, from x in base.DocumentPresenter.Document.EditingFields.OfType<CheckEditingField>()
                where (x.Brick is ICheckBoxBrick) && (x.GroupID == this.EditingField.GroupID)
                select x, new Action(base.DocumentPresenter.Update));
        }

        protected override bool PostEditorCore() => 
            true;

        protected override DataTemplate SelectTemplate()
        {
            DataTemplate template = new DataTemplate {
                VisualTree = new FrameworkElementFactory(typeof(CheckEdit))
            };
            template.VisualTree.SetValue(BaseEdit.EditValueProperty, new System.Windows.Data.Binding("Value"));
            return template;
        }

        protected CheckEditingField EditingField =>
            (CheckEditingField) base.EditingField;

        protected CheckBoxEditorInitialData InitialData =>
            (CheckBoxEditorInitialData) base.InitialData;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CheckInplaceEditor.<>c <>9 = new CheckInplaceEditor.<>c();
            public static Func<CheckState, CheckState> <>9__7_0;

            internal CheckState <.ctor>b__7_0(CheckState state)
            {
                CheckState[] first = new CheckState[2];
                first[1] = CheckState.Checked;
                return first.Concat<CheckState>(CheckState.Unchecked.Yield<CheckState>()).SkipWhile<CheckState>(_ => (_ != state)).Skip<CheckState>(1).First<CheckState>();
            }
        }

        protected class CheckBoxEditorInitialData : DocumentInplaceEditorBase.InplaceEditorInitialData
        {
            private readonly CheckEditingField editingField;
            private readonly Action update;

            public CheckBoxEditorInitialData(CheckEditingField editingField, Action update) : base(editingField.EditValue)
            {
                this.editingField = editingField;
                this.update = update;
            }

            protected override void OnValueChanged(object oldValue)
            {
                if (this.editingField != null)
                {
                    this.editingField.EditValue = ((bool) base.Value) ? CheckState.Checked : CheckState.Unchecked;
                    this.update();
                }
            }
        }

        protected class RadioCheckBoxEditorInitialData : DocumentInplaceEditorBase.InplaceEditorInitialData
        {
            private readonly CheckEditingField editingField;
            private readonly IEnumerable<CheckEditingField> groupFields;
            private readonly Action update;

            public RadioCheckBoxEditorInitialData(CheckEditingField editingField, IEnumerable<CheckEditingField> groupFields, Action update) : base(editingField.EditValue)
            {
                this.editingField = editingField;
                this.groupFields = groupFields;
                this.update = update;
            }

            protected override object CoerceValue(object newValue) => 
                true;

            protected override void OnValueChanged(object oldValue)
            {
                if (this.editingField != null)
                {
                    this.editingField.EditValue = CheckState.Checked;
                    Action<CheckEditingField> action = <>c.<>9__5_0;
                    if (<>c.<>9__5_0 == null)
                    {
                        Action<CheckEditingField> local1 = <>c.<>9__5_0;
                        action = <>c.<>9__5_0 = x => x.EditValue = CheckState.Unchecked;
                    }
                    this.groupFields.Except<CheckEditingField>(this.editingField.Yield<CheckEditingField>()).ForEach<CheckEditingField>(action);
                    this.update();
                }
            }

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly CheckInplaceEditor.RadioCheckBoxEditorInitialData.<>c <>9 = new CheckInplaceEditor.RadioCheckBoxEditorInitialData.<>c();
                public static Action<CheckEditingField> <>9__5_0;

                internal void <OnValueChanged>b__5_0(CheckEditingField x)
                {
                    x.EditValue = CheckState.Unchecked;
                }
            }
        }
    }
}

